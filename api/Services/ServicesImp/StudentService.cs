using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dto;
using api.Dto.Student;
using api.Entity;
using api.ExceptionHandler;
using api.Repository;
using api.Utils;
using AutoMapper;

namespace api.Services.ServicesImp
{
    public class StudentService : IStudentService
    {
        private readonly IMapper _mapper;
        private readonly IStudentRepo _studentRepo;
        private readonly TokenHelper _tokenHelper;
        private readonly HandlePassword _handlePassword;


        public StudentService(IMapper mapper, IStudentRepo studentRepo, TokenHelper tokenHelper, HandlePassword handlePassword)
        {
            _mapper = mapper;
            _studentRepo = studentRepo;
            _tokenHelper = tokenHelper;
            _handlePassword = handlePassword;
        }
        public async Task<Student> SignUp(CreateStudentDto createStudentDto)
        {
            var existingStudent = await _studentRepo.FindUserByEmailAsync(createStudentDto.Email);
            if (existingStudent != null)
            {
                throw new BadRequestException("Email already exists");
            }
            var hashedPassword = _handlePassword.HashPassword(createStudentDto.Password);
            var newStudent = _mapper.Map<Student>(createStudentDto);
            newStudent.Password = hashedPassword;
            newStudent.Role = RoleEnum.student;
            await _studentRepo.CreateAsync(newStudent);
            return newStudent;

        }

        public  async Task<PagingResultDto<StudentDto>> GetAllStudents(QueryObject queryObject)
        {   
            
            var pagingResult = await _studentRepo.GetAllAsync(queryObject,
            where: null, 
            s => s.Major); //  includes: s => s.Major, s => s.Subject
            var studentDtos = _mapper.Map<List<StudentDto>>(pagingResult.ResultItems);
            return new PagingResultDto<StudentDto>(studentDtos, pagingResult.TotalRecords, pagingResult.PageSize, pagingResult.CurrentPage);
        }

        public async Task<string> Login(LoginDto loginDto){
            var existingStudent = await _studentRepo.FindUserByEmailAsync(loginDto.Email);
            if (existingStudent == null ||  !_handlePassword.VerifyPassword(loginDto.Password, existingStudent.Password))
            {
                throw new BadRequestException("Email or password is not valid");
            }
            var token = _tokenHelper.GenerateJwtToken(existingStudent);
            return token;
        }

        public async Task<Student> GetStudentById(int id)
        {
            var student = await _studentRepo.FindByIdAsync(id);
            if (student == null)
            {
                throw new NotFoundException("Student not found");
            }

            return student;
        }

        public async Task<Student> DeleteStudent(int id)
        {
            var student = await _studentRepo.FindByIdAsync(id);
            if (student == null)
            {
                throw new NotFoundException("Student not found");
            }

            await _studentRepo.DeleteAsync(student);
            return student;
        }

        public async Task<Student> UpdateStudent(int id, UpdateStudentDto updateStudentDto)
        {
            var existingStudent = await _studentRepo.FindByIdAsync(id);
            if (existingStudent == null)
            {
                throw new NotFoundException("Student not found");
            }
            updateStudentDto.Password = _handlePassword.HashPassword(updateStudentDto.Password);
            _mapper.Map(updateStudentDto, existingStudent);
            await _studentRepo.UpdateAsync();
            return existingStudent;

        }
    }
}