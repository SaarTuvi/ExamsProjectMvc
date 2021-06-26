using System.Collections;
using System.Collections.Generic;

namespace Common
{
    public interface ITeacher:IAppUser
    {
       IEnumerable<IClassroom> Classrooms { get; set; }
       IEnumerable<IStudent> Students { get; set; }

        IEnumerable<IExam> Exams { get; set; }
       
    }
}