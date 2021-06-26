using Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL.Repositories
{
    public class TeachersRepository
    {
        private IServiceScopeFactory _scopeFactory;
        public TeachersRepository(IServiceScopeFactory scopeFactory)
        {
            this._scopeFactory = scopeFactory;
        }

        public int GetTeacherId(string loginName, string password)
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                ExamsAppContext context = scope.ServiceProvider.GetRequiredService<ExamsAppContext>();
                int id = context.Teachers
                .Where(st => st.LoginName == loginName && st.Password == password)
                .Select(st => st.ID).FirstOrDefault();
                return id;
            }
        }

        public ITeacher GetTeacherById(int id)
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                ExamsAppContext context = scope.ServiceProvider.GetRequiredService<ExamsAppContext>();
                ITeacher teacher = context.Teachers
                    .Where(t => t.ID == id)
                    .Include(t => t.Exams)
                    .Include(t => t.Classrooms)
                    .Include(t => t.Students)
                    .FirstOrDefault();

                return teacher;

            }
        }
    }
}
