using DAL;
using DAL.Models;
using DAL.Models.AppUsers;
using ExamsProjectMvc.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Timers;
using System.Threading.Tasks;

namespace ExamsProjectMvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ExamsAppContext _context;
        // private Timer timer;

        public HomeController(ILogger<HomeController> logger, ExamsAppContext context)
        {
            _logger = logger;
            _context = context;
        }
        public IActionResult Index()
        {
            string userType = HttpContext.Request.Cookies["userType"];
            ViewData["loged"] = "true";
            if (userType == Enums.UserType.Teacher.ToString())
            {
                return RedirectToAction("Index", "Teachers");
            }
            if (userType == Enums.UserType.Student.ToString())
            {
                return RedirectToAction("Index", "Students");
            }
            ViewData["loged"] = "false";
            return RedirectToAction("Login", "Students");
        }


        public IActionResult Logout()
        {
            string userId = HttpContext.Request.Cookies["userId"];
            if (userId != null)
            {
                foreach (var cookie in HttpContext.Request.Cookies)
                {
                    Response.Cookies.Delete(cookie.Key);
                }
            }
            return RedirectToAction("Index");
        }
        //public IActionResult Index()
        //{
        //    //IAppUser teacher = _context.Teachers.FirstOrDefault();
        //    IStudent student = _context.Students.Where(s => s.Name == "Student1")
        //        .Include(e => e.StudentExamsCollection)
        //        .ThenInclude(e =>e.Exam)
        //        .Include(t => t.Teachers)
        //        .Include(s => s.Classrooms)
        //        .ThenInclude(e => e.Exams)
        //        .ThenInclude(e => e.Questions)
        //        .ThenInclude(q => q.AnswerChoises)
        //        .FirstOrDefault();
        //    List<Student_Exam> examsToStudent = new List<Student_Exam>();
        //    foreach (var classroom in student.Classrooms)
        //    {
        //        foreach (var exam in classroom.Exams)
        //        {
        //            List<Student_Exam_Question> examQuestions = new List<Student_Exam_Question>();
        //            //examQuestions = exam.Questions.ToList();
        //            foreach (var quest in exam.Questions)
        //            {
        //                examQuestions.Add(new Student_Exam_Question
        //                {
        //                    Question = quest as Question,
        //                    IsQuestionAnswered = false
        //                });
        //            }

        //            int id = student.StudentExamsCollection
        //                .Where(x => x.ExamID == exam.ExamID)
        //                .Select(se => se.Student_ExamID)
        //                .FirstOrDefault();

        //            examsToStudent.Add(new Student_Exam
        //            {
        //                Exam = exam as Exam,
        //                Questions = examQuestions,
        //                IsAnswered = true,
        //                Grade = 80,
        //                Student_ExamID = id
        //            }) ;
        //        }
        //    }
        //    student.StudentExamsCollection = examsToStudent;
        //    _context.Students.Update(student as Student);
        //    _context.SaveChanges();

        //    IExam examForUI = student.StudentExamsCollection.First().Exam;
        //    List<IQuestion> questions = examForUI.Questions.ToList();
        //    List<AnswerChoise> answerChoises = questions[0].AnswerChoises.ToList();

        //    ViewData["Exam"] = examForUI.Title;
        //    ViewData["Question"] = questions[0].QuestionText;

        //    ViewData["FirstChoice"] = answerChoises[0].AnswerChoiceText;
        //    ViewData["SecondChoice"] = answerChoises[1].AnswerChoiceText;

        //    ViewData["Class"] = student.Classrooms.First().Title;

        //    ViewData["Teacher"] = student.Teachers
        //        .First()
        //        .Name;
        //    ViewData["Student"] = student.Name;
        //    return View();

        //    }

        //public IActionResult Index()
        //{
        //    timer = new Timer(3000);
        //    timer.Elapsed += Timer_Elapsed;
        //    timer.Enabled = true;
        //    timer.Start();
        //    return Ok("text");
        //}

        //private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        //{
        //    timer.Stop();
        //    timer.Dispose();
        //    RedirectToAction("chagePage");
        //}

        //public IActionResult changePage()
        //{
        //    return Ok("Time Elapsed");
        //}

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult AddMock()
        {
            try
            {
                Mock.CreateDb(_context);
                return Ok();
            }
            catch (Exception ex)
            {
                return Error();
            }

            //Student student = new Student();
            //student.Name = "AddedStudent";
            //student.LoginName = "Login";
            //student.Password = "1111";

            //Teacher teacher = new Teacher();
            //teacher.Name = "AddedTeacher";
            //teacher.LoginName = "Login";
            //teacher.Password = "1111";

            //Classroom classroom = new Classroom();
            //classroom.Title = "Added via code";

            //classroom.Teachers = new List<Teacher>
            //{
            //    teacher
            //};
            //student.Classrooms = new List<Classroom>{
            //    classroom };

            //try
            //{
            //    _context.Students.Add(student);
            //    _context.Teachers.Add(teacher);
            //    _context.Classrooms.Add(classroom);
            //    _context.SaveChanges();
            //    return Ok();
            //}
            //catch (Exception e)
            //{
            //    return Error();
            //}




        }
    }
}
