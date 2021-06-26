using DAL.Models;
using DAL.Models.AppUsers;
using Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL.Repositories
{
    public class Student_ExamRepository
    {
        private IServiceScopeFactory _scopeFactory;

        public Student_ExamRepository(IServiceScopeFactory scopeFactory)
        {
            this._scopeFactory = scopeFactory;
        }

        public Student_Exam GetStudentExamById(int studentExamId)
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                ExamsAppContext context = scope.ServiceProvider.GetRequiredService<ExamsAppContext>();
                return Get(studentExamId, context);
            }
        }

        public Student_Exam GetStudentExam(int examId, int studentId)
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                ExamsAppContext context = scope.ServiceProvider.GetRequiredService<ExamsAppContext>();
                int id = context.Studens_Exams
                .Where(se => se.ExamID == examId && se.UserID == studentId)
                .Select(se => se.Student_ExamID).FirstOrDefault();
                return this.Get(id, context);
            }
        }

        private Student_Exam Get(int id, ExamsAppContext context)
        {
            return context.Studens_Exams
                   .Where(se => se.Student_ExamID == id)
                   .Include(se => se.Exam)
                      .ThenInclude(e => e.Classroom)
                   .Include(se => se.Exam)
                      .ThenInclude(e => e.Teacher)
                   .Include(se => se.User)
                  .Include(e => e.Questions)
                      .ThenInclude(q => q.Question)
                      .ThenInclude(a => a.AnswerChoises)
                   .FirstOrDefault();
        }

        internal Student_Exam CreateStudentExam(int studentId, int examId, int classroomId)
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                ExamsAppContext context = scope.ServiceProvider.GetRequiredService<ExamsAppContext>();
                Student student = context.Students
                   .Where(st => st.ID == studentId)
                        .Include(e => e.StudentExamsCollection)
                            .ThenInclude(e => e.Exam)
                        .Include(t => t.Teachers)
                        .Include(s => s.Classrooms)
                            .ThenInclude(e => e.Exams)
                            .ThenInclude(q => q.Questions)
                            .ThenInclude(q => q.AnswerChoises)
                    .FirstOrDefault();

                Student_Exam studentExam =
                              student.StudentExamsCollection
                              .Where(se => se.ExamID == examId).FirstOrDefault();
                if (studentExam == null)
                {
                    Exam exam = student.Classrooms
                        .Where(cl => cl.ClassroomID == classroomId).FirstOrDefault().
                        Exams.Where(ex => ex.ExamID == examId).FirstOrDefault();
                    studentExam = ModelFactory.CreateStudentExam(studentId, exam);
                    studentExam.IsAttended = true;
                    context.Studens_Exams.Add(studentExam);
                }
                context.Students.Update(student);
                context.SaveChanges();
                return studentExam;
            }
        }

        internal int UpdateStudentExam(Student_Exam seToUpdate)
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                ExamsAppContext context = scope.ServiceProvider.GetRequiredService<ExamsAppContext>();
                Student student = context.Students
                    .Where(st => st.ID == seToUpdate.UserID)
                         .Include(e => e.StudentExamsCollection)
                             .ThenInclude(e => e.Exam)
                         .Include(e => e.StudentExamsCollection)
                            .ThenInclude(e => e.Questions)
                         .Include(t => t.Teachers)
                         .Include(s => s.Classrooms)
                             .ThenInclude(e => e.Exams)
                             .ThenInclude(q => q.Questions)
                             .ThenInclude(q => q.AnswerChoises)
                     .FirstOrDefault();

                foreach (var se in student.StudentExamsCollection)
                {
                    if(se.Student_ExamID == seToUpdate.Student_ExamID)
                    {
                        if (seToUpdate.IsSubmited)
                        {
                            se.IsSubmited = true;
                        }
                        foreach (var studentQuestion in se.Questions)
                        {
                            Student_Exam_Question question = seToUpdate.Questions
                                .Where(q => q.QuestionID == studentQuestion.QuestionID)
                                .FirstOrDefault();
                            studentQuestion.StudentAnswer = question.StudentAnswer;
                            studentQuestion.IsCorrect = question.IsCorrect;
                            studentQuestion.IsQuestionAnswered = question.IsQuestionAnswered;
                                
                        }
                        se.Grade = seToUpdate.Grade;
                        break;
                    }
                }

                context.Students.Update(student);
                context.SaveChanges();
                return seToUpdate.Student_ExamID;
            }
        }
    }
}
