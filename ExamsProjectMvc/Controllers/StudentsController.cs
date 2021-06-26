using DAL;
using Common;
using ExamsProjectMvc.Models;
using ExamsProjectMvc.Models.StudentsModels;
using ExamsProjectMvc.Models.TeachersModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExamsProjectMvc.Controllers
{
    public class StudentsController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public StudentsController(ILogger<HomeController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            string studentId = HttpContext.Request.Cookies["userId"];
            if (studentId != null)
            {
                IStudent student = _unitOfWork.GetStudentById(int.Parse(studentId));
                if (student != null)
                {
                    StudentIndexViewModel vm = ViewModelsFactory.CreateStudentIndexViewModel(student);
                    return View(vm);
                }
                Response.Cookies.Delete("userId");
            }
            return RedirectToAction("Login");
        }
        [HttpGet]
        public IActionResult Login()
        {
            string studentId = HttpContext.Request.Cookies["userId"];
            if (studentId != null)
                return RedirectToAction("Index");

            return View("StudentLogin", new LoginViewModel());
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel vm)
        {
            if (ModelState.IsValid)
            {
                int studentid = _unitOfWork.GetStudentId(vm.LoginName, vm.Password);
                if (studentid != 0)
                {
                    CookieOptions options = new CookieOptions();
                    if (vm.StayConnected)
                    {
                        options.Expires = DateTime.Now.AddDays(7);
                    }
                    HttpContext.Response.Cookies.Append("userId", studentid.ToString(), options);
                    HttpContext.Response.Cookies.Append("userType", Enums.UserType.Student.ToString(), options);
                    HttpContext.Response.Cookies.Append("isLoged", "true", options);

                    return RedirectToAction("Index");
                }
                vm.IsAttempFailed = true;
                ViewData["errorMessage"] = "Incorrect Login Name or Password";
            }
            return View("StudentLogin", vm);
        }

        public IActionResult Logout()
        {
            return RedirectToAction("Logout", "Home");
        }

        public IActionResult ReviewExam(int id)
        {
            string studentId = HttpContext.Request.Cookies["userId"];
            if (studentId != null)
            {
                IStudent_Exam studentExam = _unitOfWork.GetStudentExamById(id);
                if (studentExam != null && studentExam.UserID == int.Parse(studentId))
                {
                    return View(studentExam);
                }
                else
                {
                    return NotFound();
                }
            }
            return RedirectToAction("Login");
        }

        [HttpGet]
        public IActionResult TakeExam(int id)
        {
            string studentId = HttpContext.Request.Cookies["userId"];
            if (studentId != null)
            {
                IStudent studentLogged = _unitOfWork.GetStudentById(int.Parse(studentId));
                IExam examToTake = null;
                IStudent_Exam se = studentLogged.StudentExamsCollection.Select(se => se)
                    .Where(se => se.ExamID == id)
                    .FirstOrDefault();
                //If student started the exam before
                if (se != null && se.IsSubmited) //Exam exist and turned in
                {
                    return RedirectToAction("Index");
                }
                if (se != null && !se.IsSubmited) // Student started exam but didn't turn in
                {
                    DateTime examEndTime =
                        se.Exam.StartTime.AddMinutes(se.Exam.ExamDurationInMinutes);
                    if (DateTime.Now < examEndTime)
                    {
                        //Exam still in progress - show exam
                        TakeExamViewModel vm = ViewModelsFactory.CreateTakeExamViewModel(se);
                        return View(vm);
                    }
                    else //Exam Ended - turn in
                    {
                        int seId = SubmitExam(se);
                        return RedirectToAction("Index");
                    }
                }
                foreach (var classroom in studentLogged.Classrooms)
                {
                    examToTake =
                        classroom.Exams.Where(ex => ex.ExamID == id).FirstOrDefault();
                    if (examToTake != null)
                    {
                        break;
                    }
                }
                //Student didn't start the exam
                bool isStarted = examToTake.StartTime < DateTime.Now ? true : false;
                if (studentLogged != null && examToTake != null && isStarted)
                {
                    IStudent_Exam studentExam = _unitOfWork
                        .CreateStudentExam(studentLogged.ID, examToTake.ExamID, examToTake.ClassroomID);

                    ////Check if exam ended
                    DateTime examEndTime =
                    studentExam.Exam.StartTime.AddMinutes(studentExam.Exam.ExamDurationInMinutes);
                    if (DateTime.Now < examEndTime)
                    {
                        //Exam still in progress - show exam
                        TakeExamViewModel vm = ViewModelsFactory.CreateTakeExamViewModel(studentExam);
                        return View(vm);
                    }
                    else //Exam Ended - submit
                    {
                        se.IsAttended = false;
                        int seId = SubmitExam(se);
                        return RedirectToAction("Index");
                    }
                }
                else //no student or no exam
                {
                    return RedirectToAction("Index");
                }
            }
            return RedirectToAction("Login");
        }

        [HttpPost]
        public IActionResult TakeExam(TakeExamViewModel vm)
        {
            try
            {
                //Submit Exam To DB
                IStudent_Exam se = _unitOfWork.GetStudentExamById(vm.Student_ExamID);
                UpdateStudentExam(se, vm);
                int seId = SubmitExam(se);
            }
            catch (Exception e)
            {
                return NotFound();
            }
            return RedirectToAction("Index");
        }

        [HttpPut]
        public IActionResult SaveProgress([FromBody] TakeExamViewModel vm)
        {
            try
            {
                //Save exam to db
                IStudent_Exam se = _unitOfWork.GetStudentExamById(vm.Student_ExamID);
                int seId;
                string message = "";
                UpdateStudentExam(se, vm);
                if (se.Exam.StartTime.AddMinutes(se.Exam.ExamDurationInMinutes) <= DateTime.Now) // Exam ended
                {
                    seId = SubmitExam(se);
                    message = $"exam Ended - DataBase Updated - {seId}";
                }
                else //Exam still in progress
                {
                    seId = _unitOfWork.UpdateStudentExam(se);
                    message = $"DataBase Updated - {seId}";
                }
                return Ok(message);
            }
            catch (Exception e)
            {
                return NotFound();
            }
        }

        public IActionResult Details(int id)
        {
            string studentId = HttpContext.Request.Cookies["userId"];
            if (studentId != null)
            {
                try
                {
                    IStudent student = _unitOfWork.GetStudentById(int.Parse(studentId));
                    if (student != null)
                    {
                        foreach (var classroom in student.Classrooms)
                        {
                            IExam exam = classroom.Exams.Where(e => e.ExamID == id).FirstOrDefault();
                            if (exam != null)
                            {
                                if (exam.StartTime.Date > DateTime.Now.Date)
                                {
                                    exam.Questions = null;
                                    ExamViewModel vm = ViewModelsFactory.CreateExamViewMode(exam);
                                    return View("ExamDetails", vm);
                                }
                            }
                        }
                    }
                    return NotFound();
                }
                catch (Exception e)
                {
                    return View();
                }
            }
            return RedirectToAction("Login");
        }

        private void UpdateStudentExam(IStudent_Exam se, TakeExamViewModel vm)
        {
            var vmQuestionsList = vm.StudentQuestions.ToList();
            int i = 0;
            foreach (var studentQuestion in se.Questions)
            {

                if (vmQuestionsList[i].UserAnswer != null)
                {
                    studentQuestion.StudentAnswer = vmQuestionsList[i].UserAnswer;
                    studentQuestion.IsQuestionAnswered = true;
                }
                i++;
            }
        }

        private int SubmitExam(IStudent_Exam se)
        {
            se.IsSubmited = true;
            se.GradeExam();
            return _unitOfWork.UpdateStudentExam(se);
        }
    }
}
