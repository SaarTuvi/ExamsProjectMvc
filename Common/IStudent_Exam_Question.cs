namespace Common
{
    public interface IStudent_Exam_Question
    {
        bool IsCorrect { get; set; }
        bool IsQuestionAnswered { get; set; }
        IQuestion Question { get; set; }
        int QuestionID { get; set; }
        int Student_Exam_QuestionID { get; set; }
        int Student_ExamID { get; set; }
        string StudentAnswer { get; set; }
        IStudent_Exam StudentExam { get; set; }
    }
}