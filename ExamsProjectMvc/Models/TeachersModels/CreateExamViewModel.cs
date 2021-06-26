using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Common;

namespace ExamsProjectMvc.Models.TeachersModels
{
    public class CreateExamViewModel : ExamViewModel, IExamBase
    {
        public int ExamID { get; set; }
        public IEnumerable<IClassroom> Classrooms { get; set; }

        public int ClassroomID { get; set; }

        public string MinDate { get; set; }
        public string MaxDate { get; set; }

        IEnumerable<IQuestion> IExamBase.Questions
        {
            get { return Questions; }
            set { Questions = value as IEnumerable<QuestionViewModel>; }
        }
    }
}
