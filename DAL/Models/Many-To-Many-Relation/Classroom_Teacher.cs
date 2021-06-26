using DAL.Models.AppUsers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DAL.Models
{
   
    public class Classroom_Teacher
    {
        [Key]
        public int Classroom_TeacherIDs { get; set; }
        public int UserID { get; set; }
        public Teacher User { get; set; }
        public int ClassroomID { get; set; }
        public Classroom Classroom { get; set; }
    }
}
