using Common;
using DAL;
using DAL.Models;
using ExamsProjectMvc.Models;
using ExamsProjectMvc.Models.TeachersModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace ExamsProjectMvc.Controllers
{
    public class TeachersController : Controller
    {

        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public TeachersController(ILogger<HomeController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            string teacherId = HttpContext.Request.Cookies["userId"];
            if (teacherId != null)
            {
                ITeacher teacher = _unitOfWork.GetTeacherById(int.Parse(teacherId));
                if (teacher != null)
                {
                    TeacherIndexViewModel vm = ViewModelsFactory.CreateTeacherIndexViewModel(teacher);
                    // ViewData["loged"] = "true";

                    return View(vm);
                }
                Response.Cookies.Delete("userId");
            }
            return RedirectToAction("Login");

        }

        [HttpGet]
        public IActionResult Login()
        {
            string teacherId = HttpContext.Request.Cookies["userId"];
            if (teacherId != null)
            {
                return RedirectToAction("Index");
            }
            return View("TeachersLogin", new LoginViewModel());
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel vm)
        {
            if (ModelState.IsValid)
            {
                int studentid = _unitOfWork.GetTeacherId(vm.LoginName, vm.Password);
                if (studentid != 0)
                {
                    CookieOptions options = new CookieOptions();
                    if (vm.StayConnected)
                    {
                        options.Expires = DateTime.Now.AddDays(7);
                    }
                    HttpContext.Response.Cookies.Append("userId", studentid.ToString(), options);
                    HttpContext.Response.Cookies.Append("userType", Enums.UserType.Teacher.ToString(), options);
                    HttpContext.Response.Cookies.Append("isLoged", "true", options);


                    return RedirectToAction("Index");
                }
                vm.IsAttempFailed = true;
                ViewData["errorMessage"] = "Incorrect Login Name or Password";
            }
            return View("TeachersLogin", vm);
        }
        public IActionResult Logout()
        {
            return RedirectToAction("Logout", "Home");
        }

        public IActionResult CreateExam()
        {
            string teacherId = HttpContext.Request.Cookies["userId"];
            if (teacherId != null)
            {
                ITeacher teacher = _unitOfWork.GetTeacherById(int.Parse(teacherId));
                if (teacher != null)
                {
                    CreateExamViewModel vm = ViewModelsFactory.CreateCreateExamViewModel(teacher);
                    return View(vm);
                }
            }
            return RedirectToAction("Login");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateExam(CreateExamViewModel vm)
        {
            if (ModelState.IsValid)
            {
                foreach (var question in vm.Questions)
                {
                    foreach (var answer in question.AnswerChoises)
                    {
                        if (answer.AnswerChoiceText == question.CorrectAnswerText)
                        {
                            answer.IsCorrectAnswer = true;
                        }
                    }
                }
                try
                {
                    int examId = _unitOfWork.AddExam(vm);
                    return RedirectToAction("Index");
                }
                catch (Exception e)
                {
                    return NotFound();
                }
            }
            return View(vm);
        }

        [HttpGet]
        public IActionResult EditExam(int id)
        {
            string teacherId = HttpContext.Request.Cookies["userId"];
            if (teacherId != null)
            {
                ITeacher teacher = _unitOfWork.GetTeacherById(int.Parse(teacherId));
                if (teacher != null)
                {
                    try
                    {

                        IExam examToEdit = _unitOfWork.GetExamById(id);
                        if (examToEdit != null)
                        {
                            if (examToEdit.TeacherID == teacher.ID)
                            {
                                CreateExamViewModel vm = ViewModelsFactory.CreateCreateExamViewModel(teacher, examToEdit);
                                var questionsJson = JsonConvert.SerializeObject(vm.Questions);
                                ViewData["ExamQuestions"] = questionsJson;
                                return View(vm);
                            }
                        }
                        return NotFound();
                    }
                    catch (Exception e)
                    {
                        return NotFound();
                    }
                }
            }
            return RedirectToAction("Login");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditExam(CreateExamViewModel vm)
        {
            if (ModelState.IsValid)
            {
                foreach (var question in vm.Questions)
                {
                    foreach (var answer in question.AnswerChoises)
                    {
                        if (answer.AnswerChoiceText == question.CorrectAnswerText)
                        {
                            answer.IsCorrectAnswer = true;
                        }
                    }
                }
                try
                {
                    int examId = _unitOfWork.EditExam(vm);
                    return RedirectToAction("Index");
                }
                catch (Exception e)
                {
                    return NotFound();
                }
            }
            return View(vm);
        }

        public IActionResult ExamStatistics(int id)
        {
            string teacherId = HttpContext.Request.Cookies["userId"];
            if (teacherId != null)
            {
                IExam exam = _unitOfWork.GetExamById(id);
                if (exam != null)
                {
                    DateTime examEndTime = exam.StartTime.AddMinutes(exam.ExamDurationInMinutes);
                    if (exam.TeacherID == int.Parse(teacherId) && examEndTime < DateTime.Now)
                    {
                        ExamStatsViewModel vm = ViewModelsFactory.CreateExamStatsViewModel(exam);
                        return View("ExamStatistics", vm);

                    }
                    return RedirectToAction("Index"); //If exam still in progress show index
                }
                return NotFound(); //Show some alert
            }
            return RedirectToAction("Login");
        }

        public IActionResult ReviewExam(int id, int studentId)
        {
            string teacherId = HttpContext.Request.Cookies["userId"];
            if (teacherId != null)
            {
                try
                {
                    IStudent_Exam se = _unitOfWork.GetStudentExam(id, studentId);
                    if (se != null && se.Exam.TeacherID == int.Parse(teacherId))
                    {
                        return View(se);
                    }
                    return NotFound();
                }
                catch (Exception e)
                {
                    return NotFound();
                }
            }
            return RedirectToAction("Login");
        }

        public ActionResult DeleteExam(int id)
        {
            string teacherId = HttpContext.Request.Cookies["userId"];
            if (teacherId != null)
            {
                IExam examToDelete = _unitOfWork.GetExamById(id);
                if (examToDelete != null)
                {
                    if (examToDelete.TeacherID == int.Parse(teacherId))
                    {
                        ExamViewModel vm = ViewModelsFactory.CreateExamViewMode(examToDelete);
                        return View(vm);
                    }
                }
                return NotFound();
            }
            return RedirectToAction("Login");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteExam(int ExamId, int TeacherID, object exam)
        {
            string teacherId = HttpContext.Request.Cookies["userId"];
            if (teacherId != null)
            {
                try
                {
                    if (TeacherID == int.Parse(teacherId))
                    {
                        _unitOfWork.DeleteExam(ExamId);
                        return RedirectToAction(nameof(Index));
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

        public IActionResult Details(int id)
        {
            {
                string teacherId = HttpContext.Request.Cookies["userId"];
                if (teacherId != null)
                {
                    try
                    {
                        IExam exam = _unitOfWork.GetExamById(id);
                        if (exam != null)
                        {
                            if (int.Parse(teacherId) == exam.TeacherID)
                            {
                                ExamViewModel vm = ViewModelsFactory.CreateExamViewMode(exam);
                                return View("ExamDetails", vm);
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
        }
    }
}
