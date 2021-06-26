using System.Collections.Generic;

namespace Common
{
    public interface IQuestion
    {
        int QuestionID { get; set; }
        string QuestionText { get; set; }

        int ExamID { get; set; }
        IExam Exam { get; set; }

        IEnumerable<IAnswerChoise> AnswerChoises { get; set; }
        string CorrectAnswerText { get; set; }
    }
}