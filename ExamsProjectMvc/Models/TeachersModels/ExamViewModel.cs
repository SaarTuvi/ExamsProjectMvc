using Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ExamsProjectMvc.Models.TeachersModels
{
    public class ExamViewModel : IExamBase
    {
        public int ExamID { get; set; }
        public int TeacherID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        [Display(Name = "Classroom")]
        public int ClassroomID { get; set; }
        public string ClassroomTitle { get; set; }
        [Display(Name = "Exam Duration (in minutes)")]
        public int ExamDurationInMinutes { get; set; }
        [Display(Name = "Date and start time")]
        public DateTime StartTime { get; set; }
        public IEnumerable<QuestionViewModel> Questions { get; set; }
        IEnumerable<IQuestion> IExamBase.Questions 
        { 
            get => Questions; 
            set => Questions = value as IEnumerable<QuestionViewModel>;
        }
    }
}
