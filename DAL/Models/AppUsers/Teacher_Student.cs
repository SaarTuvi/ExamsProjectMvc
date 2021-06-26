using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DAL.Models.AppUsers
{
    public class Teacher_Student
    {
        [Key]
        public int Teacher_StudentID { get; set; }
        public int TeacherID { get; set; }
        public Teacher Teacher {get;set;}
        public int StudentId { get; set; }
        public Student Student { get; set; }
    }
}
