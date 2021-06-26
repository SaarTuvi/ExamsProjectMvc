using Common;
using ExamsProjectMvc.Models.StudentsModels;
using ExamsProjectMvc.Models.TeachersModels;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace ExamsProjectMvc.Models
{
    public static class ViewModelsFactory
    {

        #region Students View Models

        public static StudentIndexViewModel CreateStudentIndexViewModel(IStudent studentModel)
        {
            StudentIndexViewModel vm = new StudentIndexViewModel();
            vm.LoginName = studentModel.LoginName;
            vm.Name = studentModel.Name;
            vm.Teachers = studentModel.Teachers.ToList();
            vm.Classrooms = studentModel.Classrooms.ToList();
            vm.PastStudentExams =
                studentModel.StudentExamsCollection
                .OrderByDescending(se => se.Exam.StartTime)
                .ToList();

            foreach (var classroom in vm.Classrooms)
            {
                foreach (var exam in classroom.Exams)
                {
                    DateTime now = DateTime.Now;
                    if (exam.StartTime.Date > now.Date)
                    {
                        vm.FutureExams.Add(exam);
                    }

                    if (exam.StartTime.Date == now.Date)
                    {
                        var studentExam = vm.PastStudentExams
                            .Where(se => se.ExamID == exam.ExamID)
                            .Select(s => s).FirstOrDefault();
                        DateTime examEndTime = exam.StartTime.AddMinutes(exam.ExamDurationInMinutes);
                        bool isExamEnded = examEndTime > now ? false : true;
                        if (studentExam == null)
                        {
                            vm.TodayExams.Add(exam);
                        }
                        else if (!isExamEnded && !studentExam.IsSubmited)
                        {
                            vm.TodayExams.Add(exam);
                            vm.PastStudentExams.Remove(studentExam);
                        }
                    }
                }
            }
            return vm;
        }
        public static TakeExamViewModel CreateTakeExamViewModel(IStudent_Exam se)
        {
            List<StudentQuestionViewModel> seQuestions = new List<StudentQuestionViewModel>();
            foreach (IQuestion question in se.Exam.Questions)
            {
                List<string> answerChoises = new List<string>();
                foreach (IAnswerChoise ac in question.AnswerChoises)
                {
                    answerChoises.Add(ac.AnswerChoiceText);
                }
                string studentAnswer = se.Questions
                    .Where(q => q.QuestionID == question.QuestionID)
                    .Select(q => q.StudentAnswer).FirstOrDefault();
                seQuestions.Add(new StudentQuestionViewModel()
                {
                    QuestionText = question.QuestionText,
                    Answers = answerChoises,
                    UserAnswer = studentAnswer
                });
            }

            TakeExamViewModel vm = new TakeExamViewModel()
            {
                StudentId = se.UserID,
                ExamID = se.ExamID,
                Student_ExamID = se.Student_ExamID,
                Description = se.Exam.Description,
                Title = se.Exam.Title,
                StudentQuestions = seQuestions,
                StartTime = se.Exam.StartTime,
                ExamDurationInMinutes = se.Exam.ExamDurationInMinutes
            };
            return vm;
        }
        #endregion

        #region Teachers View Models
        public static TeacherIndexViewModel CreateTeacherIndexViewModel(ITeacher teacherModel)
        {
            TeacherIndexViewModel vm = new TeacherIndexViewModel();
            vm.LoginName = teacherModel.LoginName;
            vm.Name = teacherModel.Name;
            vm.Classrooms = teacherModel.Classrooms.ToList();
            List<IExam> exams = teacherModel.Exams.ToList();
            List<IExam> pastExams = new List<IExam>();
            List<IExam> futureExams = new List<IExam>();

            foreach (var exam in exams)
            {
                DateTime now = DateTime.Now;
                bool isEnded =
                    exam.StartTime.AddMinutes(exam.ExamDurationInMinutes) > now ? false : true;
                if (exam.StartTime.Date > now.Date)
                {
                    futureExams.Add(exam);
                }
                else if (exam.StartTime.Date == now.Date && !isEnded)
                {
                    vm.TodayExams.Add(exam);
                }
                else
                {
                    pastExams.Add(exam);
                }
            }
            vm.PastExams = pastExams.OrderByDescending(e => e.StartTime).ToList();
            vm.FutureExams = futureExams.OrderBy(e => e.StartTime).ToList();
            return vm;
        }
        public static CreateExamViewModel CreateCreateExamViewModel(ITeacher teacher)
        {

            var minDate = DateTime.Today.AddHours(8).AddDays(1);
            var maxDate = minDate.AddYears(10);

            string minDateString = minDate.ToString("s", CultureInfo.CreateSpecificCulture("fr-FR"));
            minDateString = minDateString.Remove(minDateString.Length - 3);
            string maxDateString = maxDate.ToString("s", CultureInfo.CreateSpecificCulture("fr-FR"));
            maxDateString = maxDateString.Remove(maxDateString.Length - 3);

            CreateExamViewModel vm = new CreateExamViewModel()
            {
                Classrooms = teacher.Classrooms.ToList(),
                TeacherID = teacher.ID,
                MinDate = minDateString,
                MaxDate = maxDateString,
                StartTime = minDate
            };
            return vm;
        }
        public static CreateExamViewModel CreateCreateExamViewModel(ITeacher teacher, IExam examToEdit)
        {
            var minDate = DateTime.Today.AddHours(8).AddDays(1);
            var maxDate = minDate.AddYears(10);

            string minDateString = minDate.ToString("s", CultureInfo.CreateSpecificCulture("fr-FR"));
            minDateString = minDateString.Remove(minDateString.Length - 3);
            string maxDateString = maxDate.ToString("s", CultureInfo.CreateSpecificCulture("fr-FR"));
            maxDateString = maxDateString.Remove(maxDateString.Length - 3);

            List<QuestionViewModel> questionsList = CreateQuestionsList(examToEdit.Questions, examToEdit.ExamID);

            CreateExamViewModel vm = new CreateExamViewModel()
            {
                Classrooms = teacher.Classrooms.ToList(),
                TeacherID = teacher.ID,
                MinDate = minDateString,
                MaxDate = maxDateString,
                ClassroomID = examToEdit.ClassroomID,
                ExamDurationInMinutes = examToEdit.ExamDurationInMinutes,
                StartTime = examToEdit.StartTime,
                Description = examToEdit.Description,
                Title = examToEdit.Title,
                Questions = questionsList,
                ExamID = examToEdit.ExamID
            };
            return vm;
        }
        public static ExamStatsViewModel CreateExamStatsViewModel(IExam exam)
        {
            ExamStatsViewModel vm = new ExamStatsViewModel()
            {
                Student_Exam_Collection = exam.StudentsExam,
                ExamID = exam.ExamID,
                Title = exam.Title,
                Description = exam.Description,
                StartTime = exam.StartTime,

                ExamDurationInMinutes = exam.ExamDurationInMinutes,
                ClassroomTitle = exam.Classroom.Title,
                NumberOfStudents = exam.StudentsExam.Count()
            };

            float allGrades = 0;
            foreach (var se in exam.StudentsExam)
            {

                if (se.IsAttended)
                {
                    vm.StudentsAttended++;
                    allGrades += (float)se.Grade;
                }
            }
            vm.AvarageGradeOfAllClass = allGrades / vm.NumberOfStudents;
            vm.AvarageGradeOfAttendeed = allGrades / vm.StudentsAttended;

            return vm;

        }
        #endregion

        #region Exam View Models

        public static ExamViewModel CreateExamViewMode(IExam exam)
        {
            List<QuestionViewModel> questionsList = CreateQuestionsList(exam.Questions, exam.ExamID);
            return new ExamViewModel()
            {
                ExamID = exam.ExamID,
                TeacherID = exam.TeacherID,
                Title = exam.Title,
                Description = exam.Description,
                ClassroomTitle = exam.Classroom.Title,
                ExamDurationInMinutes = exam.ExamDurationInMinutes,
                StartTime = exam.StartTime,
                Questions = questionsList
            };
        }
        private static List<QuestionViewModel> CreateQuestionsList(IEnumerable<IQuestion> questionsList, int examId)
        {
            List<QuestionViewModel> listToReturn = new List<QuestionViewModel>();
            if (questionsList != null)
            {
                foreach (var question in questionsList)
                {
                    List<AnswerChoiceViewModel> answerChoicesList = new List<AnswerChoiceViewModel>();
                    foreach (var answer in question.AnswerChoises)
                    {
                        answerChoicesList.Add(new AnswerChoiceViewModel()
                        {
                            AnswerChoiseId = answer.AnswerChoiseId,
                            AnswerChoiceText = answer.AnswerChoiceText,
                            IsCorrectAnswer = answer.IsCorrectAnswer
                        });
                    }
                    listToReturn.Add(new QuestionViewModel
                    {
                        QuestionID = question.QuestionID,
                        QuestionText = question.QuestionText,
                        ExamID = examId,
                        CorrectAnswerText = question.CorrectAnswerText,
                        AnswerChoises = answerChoicesList
                    });
                }
            }
            return listToReturn;
        }
        #endregion

    }
}
