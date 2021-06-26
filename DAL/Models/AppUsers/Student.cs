using Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DAL.Models.AppUsers
{
    public class Student : AppUser, IStudent
    {
        public IEnumerable<Teacher> Teachers { get; set; }
        public IEnumerable<Classroom> Classrooms { get; set; }
        public IEnumerable<Student_Exam> StudentExamsCollection { get; set; }

        //[NotMapped]
        IEnumerable<ITeacher> IStudent.Teachers
        {
            get => Teachers;
            set { Teachers = value as IEnumerable<Teacher>; }
        }
        // [NotMapped]
        IEnumerable<IClassroom> IStudent.Classrooms
        {
            get => Classrooms;
            set { Classrooms = value as IEnumerable<Classroom>; }
        }

        IEnumerable<IStudent_Exam> IStudent.StudentExamsCollection
        {
            get => StudentExamsCollection;
            set => StudentExamsCollection = value as IEnumerable<Student_Exam>;
        }

        public Student()
        {
            Teachers = new List<Teacher>();
            Classrooms = new List<Classroom>();
            StudentExamsCollection = new List<Student_Exam>();

        }
    }
}
