using Common;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Models
{
    public class AnswerChoise : IAnswerChoise
    {
        [Key]
        public int AnswerChoiseId { get; set; }
        public int QuestionID { get; set; }
        [JsonIgnore]
        public Question Question { get; set; }

        public string AnswerChoiceText { get; set; }
        public bool IsCorrectAnswer { get; set; }
        [JsonIgnore]
        IQuestion IAnswerChoise.Question
        {
            get { return Question; }
            set { Question = value as Question; }
        }
    }
}