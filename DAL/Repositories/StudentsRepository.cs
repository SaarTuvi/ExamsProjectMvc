using DAL.Models;
using DAL.Models.AppUsers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL.Repositories
{
    public class StudentsRepository
    {
        private IServiceScopeFactory _scopeFactory;
        public StudentsRepository(IServiceScopeFactory scopeFactory)
        {
            this._scopeFactory = scopeFactory;
        }

        public int GetStudentId(string loginName, string password)
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                ExamsAppContext context = scope.ServiceProvider.GetRequiredService<ExamsAppContext>();
                int id = context.Students
                .Where(st => st.LoginName == loginName && st.Password == password)
                .Select(st => st.ID).FirstOrDefault();
                return id;
            }
        }

        public Student GetStudentById(int id)
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                ExamsAppContext context = scope.ServiceProvider.GetRequiredService<ExamsAppContext>();
                Student student = context.Students
                    .Where(st => st.ID == id)
                        .Include(e => e.StudentExamsCollection)
                            .ThenInclude(e => e.Exam)
                        .Include(e => e.StudentExamsCollection)
                            .ThenInclude(e => e.Questions)
                        .Include(t => t.Teachers)
                        .Include(s => s.Classrooms)
                            .ThenInclude(e => e.Exams)
                            .ThenInclude(q =>q.Questions)
                            .ThenInclude(q => q.AnswerChoises)
                    .FirstOrDefault();
                if (student != null)
                {
                    foreach (Classroom classroom in student.Classrooms)
                    {
                        foreach (Exam exam in classroom.Exams)
                        {
                            Student_Exam studentExam =
                                student.StudentExamsCollection
                                .Where(ex => ex.ExamID == exam.ExamID).FirstOrDefault();
                            DateTime examEndTime = exam.StartTime.AddMinutes(exam.ExamDurationInMinutes);
                            if (examEndTime < DateTime.Now && studentExam == null)
                            {
                                studentExam = ModelFactory.CreateStudentExam(student.ID, exam);
                                studentExam.IsSubmited = true;
                                context.Studens_Exams.Add(studentExam);
                            }
                            else if(examEndTime < DateTime.Now && !studentExam.IsSubmited)
                            {
                                studentExam.IsSubmited = true;
                                studentExam.GradeExam();
                            }
                        }
                    }
                context.Students.Update(student);
                context.SaveChanges();
                }
                return student;
            }
        }
    }
}
