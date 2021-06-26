using Common; 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExamsProjectMvc.Models.StudentsModels
{
    public class StudentIndexViewModel
    {
        public int ID { get; set; }
        public string LoginName { get; set; }
        public string Name { get; set; }

        public List<ITeacher> Teachers { get; set; } = new List<ITeacher>();
        public List<IClassroom> Classrooms { get; set; } = new List<IClassroom>();
        public List<IStudent_Exam> PastStudentExams { get; set; } = new List<IStudent_Exam>();
        public List<IExam> FutureExams { get; set; } = new List<IExam>();
        public List<IExam> TodayExams { get; set; } = new List<IExam>();
    }
}
