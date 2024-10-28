using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dto;
using api.Entity;
using api.Services.ServicesImp;
using api.Utils;

namespace api.Services
{
    public interface IStudentSubjectService
    {
        Task<List<StudentSubject>> GetSubjectOfStudent(QueryObject queryObject, int studentId);

        Task<StudentSubject> CreateStudentSubject(int studentId, int SubjectId);

        Task<StudentSubject> DeleteStudentSubject(int studentId);

    }
}