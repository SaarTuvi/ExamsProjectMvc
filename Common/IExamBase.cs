using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Common
{
    public interface IExamBase
    {
        int ExamID { get; set; }
        [Display(Name = "Title")]
        string Title { get; set; }
        [Display(Name = "Description")]
        string Description { get; set; }
        [Display(Name = "Start tIME")]
        DateTime StartTime { get; set; }
        [Display(Name = "Exam Duration")]
        int ExamDurationInMinutes { get; set; }

        int TeacherID { get; set; }
        int ClassroomID { get; set; }

        IEnumerable<IQuestion> Questions { get; set; }
    }
}
