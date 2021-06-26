using System.Collections;
using System.Collections.Generic;

namespace Common
{
    public interface IClassroom
    {
        int ClassroomID { get; set; }
        string Title { get; set; }
        IEnumerable<ITeacher> Teachers { get; set; }
        IEnumerable<IStudent> Students { get; set; }
        IEnumerable<IExam> Exams { get; set; }
    }
}