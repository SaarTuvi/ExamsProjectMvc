using DAL;
using DAL.Models;
using DAL.Models.AppUsers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExamsProjectMvc
{
    public static class Mock
    {
        public static void CreateDb(ExamsAppContext context)
        {
            #region Create Teachers + Exams

            Teacher teacher = CreateTeacher("Teacher1");
            Teacher teacher2 = CreateTeacher("Teacher2");
            teacher.Exams = CreateExams("Teacher1");
            teacher2.Exams = CreateExams("Teacher2");
            #endregion

            #region Create Classrooms

            Classroom classroom1_1 = CreateClassroom("Teacher1Class1");
            Classroom classroom1_2 = CreateClassroom("Teacher1Class1");

            Classroom classroom2_1 = CreateClassroom("Teacher2Class1");
            Classroom classroom2_2 = CreateClassroom("Teacher2Class2");
            #endregion

            #region Set Exams To Classrooms

            classroom1_1.Exams = teacher.Exams;

            List<Exam> classroom2_1Exams = new List<Exam>() { teacher2.Exams.First() };
            classroom2_1.Exams = classroom2_1Exams;

            List<Exam> classroom2_2Exams = new List<Exam>() { teacher2.Exams.Last() };
            classroom2_2.Exams = classroom2_2Exams;
            #endregion

            #region Set Classrooms To Teachers

            List<Classroom> teacher1Classrooms = new List<Classroom>() { classroom1_1, classroom1_2 };
            teacher.Classrooms = teacher1Classrooms;

            List<Classroom> teacher2Classrooms = new List<Classroom>() { classroom2_1, classroom2_2 };
            teacher2.Classrooms = teacher2Classrooms;
            #endregion

            #region Create Students

            Student student = CreateStudent("Student1");
            Student student2 = CreateStudent("Student2");
            #endregion

            #region Set Teachers To Student

            List<Teacher> student1Teachers = new List<Teacher>() { teacher, teacher2 };
            student.Teachers = student1Teachers;

            student2.Teachers = new List<Teacher>() { teacher2 };
            #endregion

            #region Set Classrooms To Students


            List<Classroom> studentClassrooms = new List<Classroom>();
            List<Classroom> student2Classrooms = new List<Classroom>();


            foreach (var teacherItem in student.Teachers)
            {
                foreach (var classroom in teacherItem.Classrooms)
                {
                    studentClassrooms.Add(classroom);
                }
            }
            foreach (var teacherItem in student2.Teachers)
            {

                foreach (var classroom in teacher2.Classrooms)
                {
                    student2Classrooms.Add(classroom);
                }
            }
            student.Classrooms = studentClassrooms;
            student2.Classrooms = student2Classrooms;
            #endregion

            #region Save In DB


            context.Teachers.Add(teacher);
            context.Teachers.Add(teacher2);

            context.Classrooms.Add(classroom1_1);
            context.Classrooms.Add(classroom1_2);
            context.Classrooms.Add(classroom2_1);
            context.Classrooms.Add(classroom2_2);

            context.Students.Add(student);
            context.Students.Add(student2);


            context.SaveChanges();
            #endregion

        }

        public static List<Exam> CreateExams(string teacherName)
        {
            List<Exam> exams = new List<Exam>();
            for (int z = 0; z < 2; z++)
            {

                Exam exam = new Exam();
                exam.Title = $"Exam #{z} - {teacherName}";
                exam.ExamDurationInMinutes = 90;
                exam.StartTime = DateTime.Now;
                exam.Description = $"Created Exam With Code";
                List<Question> questions = new List<Question>();
                for (int i = 0; i < 4; i++)
                {
                    Question question = new Question()
                    {
                        QuestionText = $"Question #{i + 1}",

                    };

                    List<AnswerChoise> answerList = new List<AnswerChoise>();
                    for (int x = 0; x < 4; x++)
                    {
                        AnswerChoise answer = new AnswerChoise()
                        {
                            AnswerChoiceText = $"AnswerChoice#{x + 1}",
                            IsCorrectAnswer = false
                        };
                        if (x == 3)
                        {
                            answer.IsCorrectAnswer = true;
                        }
                        answerList.Add(answer);
                    }
                    string correctAnswer = answerList
                        .Where(a => a.IsCorrectAnswer == true)
                        .Select(a => a.AnswerChoiceText).First();
                    question.CorrectAnswerText = correctAnswer;
                    question.AnswerChoises = answerList;
                    questions.Add(question);
                }
                exam.Questions = questions;
                exams.Add(exam);
            }
            return exams;
        }

        public static Teacher CreateTeacher(string name)
        {
            Teacher newTeacher = new Teacher()
            {
                Name = name,
                LoginName = "teacher",
                Password = "teacher"
            };
            return newTeacher;
        }

        public static Classroom CreateClassroom(string title)
        {
            Classroom newClassroom = new Classroom()
            {
                Title = title,

            };
            return newClassroom;
        }

        public static Student CreateStudent(string name)
        {
            Student newStudent = new Student();
            newStudent.Name = name;
            newStudent.LoginName = "student";
            newStudent.Password = "student";

            return newStudent;
        }
    }
}
