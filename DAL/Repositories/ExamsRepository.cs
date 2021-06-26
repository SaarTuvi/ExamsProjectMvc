using DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL.Repositories
{
    public class ExamsRepository
    {
        private IServiceScopeFactory _scopeFactory;

        public ExamsRepository(IServiceScopeFactory scopeFactory)
        {
            this._scopeFactory = scopeFactory;
        }

        public int AddExam(Exam examToAdd)
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                ExamsAppContext context = scope.ServiceProvider.GetRequiredService<ExamsAppContext>();
                context.Exams.Add(examToAdd);
                context.SaveChanges();

                return examToAdd.ExamID;
            }
        }

        public Exam GetExamById(int examId)
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                ExamsAppContext context = scope.ServiceProvider.GetRequiredService<ExamsAppContext>();
                Exam examToReturn = context.Exams.Where(e => e.ExamID == examId)
                    .Include(e => e.Teacher)
                    .Include(e => e.Classroom)
                        .ThenInclude(c => c.Students)
                    .Include(e => e.StudentsExam)
                        .ThenInclude(se => se.User)
                    .Include(e => e.StudentsExam)
                        .ThenInclude(se => se.Questions)
                    .Include(e => e.Questions).
                        ThenInclude(q => q.AnswerChoises)
                        
                    .FirstOrDefault();

                if (examToReturn != null)
                {
                    DateTime examEndTime = examToReturn.StartTime.AddMinutes(examToReturn.ExamDurationInMinutes);
                    if (examEndTime < DateTime.Now)
                    {
                        Classroom classroom = examToReturn.Classroom;
                        IEnumerable<Student_Exam> studentExamCollection = examToReturn.StudentsExam;
                        if(studentExamCollection.Count() != classroom.Students.Count())
                        {

                        foreach (var student in classroom.Students)
                        {
                            Student_Exam studentExam = studentExamCollection
                                .Where(se => se.UserID == student.ID).FirstOrDefault();
                            if(studentExam == null)
                            {
                            studentExam = ModelFactory.CreateStudentExam(student.ID, examToReturn);
                            context.Studens_Exams.Add(studentExam);
                            }
                        }
                        context.Exams.Update(examToReturn);
                        context.SaveChanges();  
                        }
                    }
                }

                return examToReturn;
            }
        }

        public int PutExam(Exam examModel)
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                ExamsAppContext context = scope.ServiceProvider.GetRequiredService<ExamsAppContext>();
                Exam examToUpdate = context.Exams.Where(e => e.ExamID == examModel.ExamID)
                    .Include(e => e.Teacher)
                    .Include(e => e.Questions).
                        ThenInclude(q => q.AnswerChoises)

                    .FirstOrDefault();

                if (examToUpdate != null)
                {
                    examToUpdate.Title = examModel.Title;
                    examToUpdate.Description = examModel.Description;
                    examToUpdate.ClassroomID = examModel.ClassroomID;
                    examToUpdate.StartTime = examModel.StartTime;
                    examToUpdate.ExamDurationInMinutes = examModel.ExamDurationInMinutes;
                    examToUpdate.Questions = examModel.Questions;
                    context.SaveChanges();
                    return examToUpdate.ExamID;
                }
                else return 0;
            }
        }

        internal void DeleteExam(int examID)
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                ExamsAppContext context = scope.ServiceProvider.GetRequiredService<ExamsAppContext>();
                Exam examToDelete = new Exam { ExamID = examID };
                context.Remove(examToDelete);
                context.SaveChanges();
            }
        }
    }
}
