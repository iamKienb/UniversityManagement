using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dto;
using api.Dto.Student;
using api.Entity;
using api.Utils;

namespace api.Services
{
    public interface IStudentService
    {
        Task<PagingResultDto<StudentDto>> GetAllStudents(QueryObject queryObject);

        Task<Student> SignUp(CreateStudentDto createStudentDto);

        Task<string> Login(LoginDto loginDto);

        Task<Student> GetStudentById(int id);

        Task<Student> DeleteStudent(int id);

        Task<Student> UpdateStudent(int id, UpdateStudentDto updateStudentDto);

    }
}