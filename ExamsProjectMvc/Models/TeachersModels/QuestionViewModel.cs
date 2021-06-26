using Common; 
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExamsProjectMvc.Models.TeachersModels
{
    public class QuestionViewModel : IQuestion
    {
        public int QuestionID { get; set; }
        public string QuestionText { get; set; }
        public int ExamID { get; set; }
        [JsonIgnore]
        public IExam Exam { get; set; }
        public IEnumerable<AnswerChoiceViewModel> AnswerChoises { get; set; }
        IEnumerable<IAnswerChoise> IQuestion.AnswerChoises
        {
            get { return AnswerChoises; }
            set { AnswerChoises = value as IEnumerable<AnswerChoiceViewModel>; }
        }
        public string CorrectAnswerText { get; set; }
    }
}
