using Common;
using DAL.Models.AppUsers;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace DAL.Models
{
    public class Student_Exam : IStudent_Exam
    {
        [Key]
        public int Student_ExamID { get; set; }
        public int UserID { get; set; }
        public Student User { get; set; }
        public int ExamID { get; set; }
        public Exam Exam { get; set; }

        public bool IsAttended { get; set; }
        public bool IsSubmited { get; set; }
        public float? Grade { get; set; }
        public IEnumerable<Student_Exam_Question> Questions { get; set; }
        IExam IStudent_Exam.Exam
        {
            get => Exam;
            set => Exam = value as Exam;
        }
        IEnumerable<IStudent_Exam_Question> IStudent_Exam.Questions
        {
            get => Questions;
            set => Questions = value as IEnumerable<Student_Exam_Question>;
        }
        IStudent IStudent_Exam.User
        {
            get => User;
            set => User = value as Student;
        }

        public Student_Exam()
        {

        }

        public void GradeExam()
        {
            int correctAnswers = 0;
            int numOfQuestions = this.Questions.Count();
            foreach (var question in this.Questions)
            {
                if (question.StudentAnswer == question.Question.CorrectAnswerText)
                {
                    question.IsCorrect = true;
                    correctAnswers++;
                }
            }

            this.Grade = 100 * correctAnswers / numOfQuestions;
        }
    }
}