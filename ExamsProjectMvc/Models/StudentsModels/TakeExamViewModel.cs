
using ExamsProjectMvc.Models.TeachersModels;
using System.Collections.Generic;

namespace ExamsProjectMvc.Models.StudentsModels
{
    public class TakeExamViewModel : ExamViewModel
    {
        public int StudentId { get; set; }
        public int Student_ExamID { get; set; }


        public IEnumerable<StudentQuestionViewModel> StudentQuestions { get; set; }


    }
}