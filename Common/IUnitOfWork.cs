using System;
using System.Collections.Generic;
using System.Text;

namespace Common
{
    public interface IUnitOfWork
    {
        int AddExam(IExamBase examToAdd);
        IStudent_Exam CreateStudentExam(int studentId, int examId, int classroomId);
        void DeleteExam(int examId);
        int EditExam(IExamBase examToEdit);
        IExam GetExamById(int examId);
        IStudent GetStudentById(int id);
        IStudent_Exam GetStudentExam(int examId, int studentId);
        IStudent_Exam GetStudentExamById(int studentExamId);
        int GetStudentId(string loginName, string password);
        ITeacher GetTeacherById(int id);
        int GetTeacherId(string loginName, string password);
        int UpdateStudentExam(IStudent_Exam se);
    }
}
