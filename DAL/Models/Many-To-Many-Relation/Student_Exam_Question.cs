using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Common;

namespace DAL.Models
{
    public class Student_Exam_Question : IStudent_Exam_Question
    {
        [Key]
        public int Student_Exam_QuestionID { get; set; }

        public int Student_ExamID { get; set; }
        public Student_Exam StudentExam { get; set; }
        public int QuestionID { get; set; }
        public Question Question { get; set; }

        public bool IsQuestionAnswered { get; set; }
        public bool IsCorrect { get; set; }
        public string StudentAnswer { get; set; }
        IQuestion IStudent_Exam_Question.Question 
        { 
            get => Question; 
            set => Question = value as Question; 
        }
        IStudent_Exam IStudent_Exam_Question.StudentExam 
        {
            get => StudentExam; 
            set => StudentExam = value as Student_Exam; 
        }
    }
}
