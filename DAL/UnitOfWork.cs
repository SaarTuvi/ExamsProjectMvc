using DAL.Models;
using Common;
using DAL.Models.AppUsers;
using DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL
{
    public class UnitOfWork : IUnitOfWork
    {
        #region Fields

        private IServiceScopeFactory _scopeFactory;
        private StudentsRepository studentsRepository;
        private TeachersRepository teachersRepository;
        private ExamsRepository examsRepository;
        private Student_ExamRepository seRepository;

        #endregion

        #region Properties

        public StudentsRepository StudentsRepository
        {
            get
            {
                if (this.studentsRepository == null)
                {
                    this.studentsRepository = new StudentsRepository(_scopeFactory);
                }
                return this.studentsRepository;
            }
        }
        public TeachersRepository TeachersRepository
        {
            get
            {
                if (this.teachersRepository == null)
                {
                    this.teachersRepository = new TeachersRepository(_scopeFactory);
                }
                return this.teachersRepository;
            }
        }

        public ExamsRepository ExamsRepository
        {
            get
            {
                if (this.examsRepository == null)
                {
                    this.examsRepository = new ExamsRepository(_scopeFactory);
                }
                return this.examsRepository;
            }
        }
        public Student_ExamRepository SeRepository
        {
            get
            {
                if (this.seRepository == null)
                {
                    this.seRepository = new Student_ExamRepository(_scopeFactory);
                }
                return this.seRepository;
            }
        }

        #endregion

        #region Ctors

        public UnitOfWork(IServiceScopeFactory scopeFactory)
        {
            this._scopeFactory = scopeFactory;
        }
        #endregion

        #region Students_Repo Methods  
        public int GetStudentId(string loginName, string password)
        {
            return StudentsRepository.GetStudentId(loginName, password);
        }
        public IStudent GetStudentById(int id)
        {
            return StudentsRepository.GetStudentById(id);
        }

        #endregion

        #region Student_Exam_Repo Methods

        public IStudent_Exam GetStudentExam(int examId, int studentId)
        {
            return SeRepository.GetStudentExam(examId, studentId);
        }
        public IStudent_Exam GetStudentExamById(int studentExamId)
        {
            return SeRepository.GetStudentExamById(studentExamId);
        }
        public IStudent_Exam CreateStudentExam(int studentId, int examId, int classroomId)
        {
            try
            {

                return SeRepository.CreateStudentExam(studentId, examId, classroomId);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public int UpdateStudentExam(IStudent_Exam se)
        {
            try
            {
                return SeRepository.UpdateStudentExam(se as Student_Exam);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        #endregion

        #region Teachers_Repo Methods
        public int GetTeacherId(string loginName, string password)
        {
            return TeachersRepository.GetTeacherId(loginName, password);
        }
        public ITeacher GetTeacherById(int id)
        {
            return TeachersRepository.GetTeacherById(id);
        }

        #endregion

        #region Exams_Rep Methods
        public IExam GetExamById(int examId)
        {
            IExam examToReturn = ExamsRepository.GetExamById(examId);
            return examToReturn;
        }

        public int AddExam(IExamBase examToAdd)
        {
            Exam examModel = ModelFactory.CreateExamModel(examToAdd);
            ExamsRepository.AddExam(examModel);
            return 0;
        }

        public int EditExam(IExamBase examToEdit)
        {
            Exam examModel = ModelFactory.CreateExamModel(examToEdit);
            ExamsRepository.PutExam(examModel);
            return examModel.ExamID;
        }
        public void DeleteExam(int examId)
        {
            ExamsRepository.DeleteExam(examId);
        }

        #endregion

    }
}
