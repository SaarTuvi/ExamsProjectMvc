using Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Newtonsoft.Json;
using DAL.Models.AppUsers;

namespace DAL.Models
{
    public class Exam :IExam
    {
        [Key]
        public int ExamID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime StartTime { get; set; }
        public int ExamDurationInMinutes { get; set; }
        public int TeacherID { get; set; }
        [JsonIgnore]
        public Teacher Teacher { get; set; }
        public int ClassroomID { get; set; }
        [JsonIgnore]
        public Classroom Classroom { get; set; }

        public IEnumerable<Question> Questions { get; set; }
        public IEnumerable<Student_Exam> StudentsExam { get; set; }
        [JsonIgnore]
        ITeacher IExam.Teacher
        {
            get { return Teacher; }
            set { Teacher = value as Teacher; }
        }
        [JsonIgnore]
        IClassroom IExam.Classroom
        {
            get { return Classroom; }
            set { Classroom = value as Classroom; }
        }
        IEnumerable<IQuestion> IExamBase.Questions
        {
            get { return Questions; }
            set { Questions = value as IEnumerable<Question>; }
        }

        IEnumerable<IStudent_Exam> IExam.StudentsExam { 
            get => StudentsExam; 
            set => StudentsExam = value as IEnumerable<Student_Exam>; 
        }
    }
}
