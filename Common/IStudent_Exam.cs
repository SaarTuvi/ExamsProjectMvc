using System.Collections.Generic;

namespace Common
{ 
    public interface IStudent_Exam
    {
        IExam Exam { get; set; }
        int ExamID { get; set; }
        float? Grade { get; set; }
        bool IsAttended { get; set; }
        bool IsSubmited { get; set; }
        IEnumerable<IStudent_Exam_Question> Questions { get; set; }
        int Student_ExamID { get; set; }
        IStudent User { get; set; }
        int UserID { get; set; }

        void GradeExam();
    }
}