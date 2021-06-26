using Common;
using DAL.Models.AppUsers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DAL.Models
{
    public class Classroom : IClassroom
    {
        [Key]
        public int ClassroomID { get; set; }
        public string Title { get; set; }
        public IEnumerable<Teacher> Teachers { get; set; }
        public IEnumerable<Student> Students { get; set; }
        public IEnumerable<Exam> Exams { get; set; }


        //[NotMapped]
        IEnumerable<IExam> IClassroom.Exams
        {
            get { return Exams; }
            set { Exams = value as IEnumerable<Exam>; }
        }
        //[NotMapped] 
        IEnumerable<ITeacher> IClassroom.Teachers
        {
            get => Teachers;
            set { Teachers = value as IEnumerable<Teacher>; }
        }
 //       [NotMapped]
        IEnumerable<IStudent> IClassroom.Students
        {
            get => Students;
            set { Students = value as IEnumerable<Student>;  }
        }
    }
}
