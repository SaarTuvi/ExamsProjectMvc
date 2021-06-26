using Common; 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExamsProjectMvc.Models.TeachersModels
{
    public class AnswerChoiceViewModel : IAnswerChoise
    {
        public int AnswerChoiseId { get ; set ; }
        public int QuestionID { get; set; }
        public IQuestion Question { get; set; }
        public string AnswerChoiceText { get; set ; }
        public bool IsCorrectAnswer { get; set; }
    }
}
