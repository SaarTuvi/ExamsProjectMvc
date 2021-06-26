using Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ExamsProjectMvc.Models.TeachersModels
{
    public class ExamStatsViewModel :ExamViewModel
    {
        public IEnumerable<IStudent_Exam> Student_Exam_Collection { get; set; }
        [Display(Name = "Avarage Grade (Of all class)")]
        public float AvarageGradeOfAllClass { get; set; }
        [Display(Name = "Avarage Grade (Amoungst students attended)")]
        public float AvarageGradeOfAttendeed { get; set; }
        [Display(Name = "Number Of Students")]
        public int NumberOfStudents { get; set; }
        [Display(Name = "Number Of Students Attended")]
        public int StudentsAttended {get; set;}
    }
}
