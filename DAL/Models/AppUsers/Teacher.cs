using Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DAL.Models.AppUsers
{
    public class Teacher : AppUser,ITeacher
    {

        public IEnumerable<Classroom> Classrooms { get; set; }
        public IEnumerable<Student> Students { get; set; }
        public IEnumerable<Exam> Exams { get; set; }
        
        /*[NotMapped]*/
        IEnumerable<IExam> ITeacher.Exams
        {
            get { return Exams; }
            set { Exams = value as IEnumerable<Exam>; }
        }
        
       /* [NotMapped] */
        IEnumerable<IClassroom> ITeacher.Classrooms
        {
            get => Classrooms;
            set { Classrooms = value as IEnumerable<Classroom>; }
        }
        //[NotMapped]
        IEnumerable<IStudent> ITeacher.Students
        {
            get => Students;
            set { Students = value as IEnumerable<Student>; }
        }
    }
}
