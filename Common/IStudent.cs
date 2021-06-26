using System.Collections;
using System.Collections.Generic;

namespace Common
{
    public interface IStudent : IAppUser
    {

        IEnumerable<ITeacher> Teachers { get; set; }
        IEnumerable<IClassroom> Classrooms { get; set; }
        IEnumerable<IStudent_Exam> StudentExamsCollection { get; set; }
    }
}