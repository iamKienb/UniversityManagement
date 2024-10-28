using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dto;
using api.Entity;
using api.ExceptionHandler;
using api.Repository;
using api.Utils;
using AutoMapper;

namespace api.Services.ServicesImp
{
    public class StudentSubjectService : IStudentSubjectService
    {
        private readonly Lazy<IStudentService> _studentService;
        private readonly Lazy<ISubjectService> _subjectService;
        private readonly IMapper _mapper;
        private readonly IStudentSubjectRepo _studentSubjectRepo;
        public StudentSubjectService(Lazy<IStudentService> studentService, Lazy<ISubjectService> subjectService, IMapper mapper, IStudentSubjectRepo studentSubjectRepo)
        {
            _studentService = studentService;
            _subjectService = subjectService;
            _mapper = mapper;
            _studentSubjectRepo = studentSubjectRepo;
        }
        public async Task<StudentSubject> CreateStudentSubject(int studentId, int subjectId)
        {
            var existStudent = await _studentService.Value.GetStudentById(studentId);
            var subject = await _subjectService.Value.GetSubjectById(subjectId);

            if (existStudent == null || subject == null)
                throw new NotFoundException("Student or Subject not found.");

            var studentSubject = new StudentSubject
            {
                StudentId = studentId,
                SubjectId = subjectId
            };

            await _studentSubjectRepo.CreateAsync(studentSubject);

            return studentSubject;
        }

        public async Task<StudentSubject> DeleteStudentSubject(int studentId)
        {
            var student = await _studentService.Value.GetStudentById(studentId);
            if (student == null)
                throw new NotFoundException("Student not found.");

            var studentOfSubject = await _studentSubjectRepo.findOneByConditionAsync(ss => ss.StudentId == studentId);


            if (studentOfSubject == null)
                throw new NotFoundException("Student Subject not found.");

            await _studentSubjectRepo.DeleteAsync(studentOfSubject);
            return studentOfSubject;
        }

        public async Task<PagingResultDto<StudentSubject>> GetSubjectOfStudent(QueryObject queryObject, int studentId)
        {
            var pagingResultDto = await _studentSubjectRepo.GetAllAsync(
                queryObject,
                where: ss => ss.StudentId == studentId,
                includes:ss => ss.Subject
            );
            if (pagingResultDto == null)
                throw new NotFoundException("No subjects found for the specified student.");

            return pagingResultDto;
        }

  
    }
}