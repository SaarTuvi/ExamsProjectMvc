using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Common
{
    public interface IExam : IExamBase
    {
        ITeacher Teacher { get; set; }
        IClassroom Classroom { get; set; }
        IEnumerable<IStudent_Exam> StudentsExam { get; set; }

    }
}