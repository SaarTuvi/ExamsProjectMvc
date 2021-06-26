using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExamsProjectMvc.Models.StudentsModels
{
    public class StudentQuestionViewModel
    {
        public string QuestionText { get; set; }
        public List<string> Answers { get; set; }
        public string UserAnswer { get; set; }
    }
}
