using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Common;

namespace DAL.Models
{
    public class Question : IQuestion
    {
        [Key]
        public int QuestionID { get; set; }
        public string QuestionText { get; set; }

        public int ExamID { get; set; }
        [JsonIgnore]
        public Exam Exam { get; set; }
        [NotMapped]
        [JsonIgnore]
        IExam IQuestion.Exam
        {
            get { return Exam; }
            set { Exam = value as Exam; }
        }
        public IEnumerable<AnswerChoise> AnswerChoises { get; set; }

        IEnumerable<IAnswerChoise> IQuestion.AnswerChoises
        {
            get { return AnswerChoises; }
            set { AnswerChoises = value as IEnumerable<AnswerChoise>; }
        }
        
        public string CorrectAnswerText { get; set; }
    }
}