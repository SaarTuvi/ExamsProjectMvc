using DAL.Models;
using DAL.Models.AppUsers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    public class ExamsAppContext :DbContext
    {
        public ExamsAppContext(DbContextOptions<ExamsAppContext> options) :base(options)
        {

        }
        //Users
        public DbSet<AppUser> Users { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Student> Students { get; set; }

        public DbSet<Classroom> Classrooms { get; set; }

        //Exam Related
        public DbSet<Exam> Exams { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<AnswerChoise> AnswerChoises { get; set; }


        //Relational Tables
        public DbSet<Student_Exam> Studens_Exams { get; set; }
        public DbSet<Student_Exam_Question> Students_Exams_Questions { get; set; }
    }
}
