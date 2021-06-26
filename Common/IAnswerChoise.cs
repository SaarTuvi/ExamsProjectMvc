namespace Common
{
    public interface IAnswerChoise
    {
        int AnswerChoiseId { get; set; }
        int QuestionID { get; set; }
        IQuestion Question { get; set; }

        string AnswerChoiceText { get; set; }
        bool IsCorrectAnswer { get; set; }
    }
}