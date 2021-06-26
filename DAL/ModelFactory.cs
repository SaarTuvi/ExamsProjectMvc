using DAL.Models;
using Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    public static class ModelFactory
    {
        public static Student_Exam CreateStudentExam(int studentId, IExam exam)
        {

            Student_Exam studentExam = new Student_Exam()
            {
                UserID = studentId,
                ExamID = exam.ExamID,
                Exam = exam as Exam,
                Grade = 0,
                IsAttended = false,
                IsSubmited = false
            };

            List<Student_Exam_Question> studentQuestionsList = new List<Student_Exam_Question>();
            foreach (var question in exam.Questions)
            {

                Student_Exam_Question studentQuestion = new Student_Exam_Question()
                {
                    //        Student_ExamID = studentExamId,
                    QuestionID = question.QuestionID,
                    IsCorrect = false,
                    IsQuestionAnswered = false
                };
                studentQuestionsList.Add(studentQuestion);
            }
            studentExam.Questions = studentQuestionsList;

            return studentExam;
        }

        public static Exam CreateExamModel(IExamBase examBase)
        {
            List<Question> questionsList = new List<Question>();
            foreach (IQuestion item in examBase.Questions)
            {
                List<AnswerChoise> answersList = new List<AnswerChoise>();
                foreach (IAnswerChoise answer in item.AnswerChoises)
                {
                    answersList.Add(new AnswerChoise()
                    {
                        AnswerChoiseId = answer.AnswerChoiseId,
                        QuestionID = answer.QuestionID,
                        AnswerChoiceText = answer.AnswerChoiceText,
                        IsCorrectAnswer = answer.IsCorrectAnswer
                    });
                }
                questionsList.Add(new Question()
                {
                    QuestionText = item.QuestionText,
                    AnswerChoises = answersList,
                    CorrectAnswerText = item.CorrectAnswerText
                });

            }
                return new Exam()
                {
                    Title = examBase.Title,
                    Description = examBase.Description,
                    StartTime = examBase.StartTime,
                    ExamDurationInMinutes = examBase.ExamDurationInMinutes,
                    TeacherID = examBase.TeacherID,
                    ClassroomID = examBase.ClassroomID,
                    Questions = questionsList,
                    ExamID = examBase.ExamID
                    
                };

            }

        }
    }

