using DAL.Models.AppUsers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DAL.Models
{
    public class Classroom_Student
    {
        [Key]
        public int Classroom_StudentID { get; set; }
        public int UserID { get; set; }
        public Student User { get; set; }
        public int ClassroomID { get; set; }
        public Classroom Classroom { get; set; }
    }
}
