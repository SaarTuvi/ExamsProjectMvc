using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExamsProjectMvc.Models.TeachersModels
{
    public class TeacherIndexViewModel
    {
        public int ID { get; set; }
        public string LoginName { get; set; }
        public string Name { get; set; }

        public List<IClassroom> Classrooms { get; set; } = new List<IClassroom>();
        public List<IExam> PastExams { get; set; } = new List<IExam>();
        public List<IExam> FutureExams { get; set; } = new List<IExam>();
        public List<IExam> TodayExams { get; set; } = new List<IExam>();
    }
}
