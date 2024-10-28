using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dto;
using api.Dto.Education;
using api.Entity;
using api.Repository;
using api.Utils;
using AutoMapper;

namespace api.Services.ServicesImp
{
    public class EducationService : IEducationService
    {
        private readonly IEducationRepo _educationRepo;
        private readonly Lazy<IStudentService> _studentService;
        private readonly Lazy<IMajorService> _majorService;

        private readonly IMapper _mapper;
        public EducationService(Lazy<IStudentService> studentService, Lazy<IMajorService> majorService, IMapper mapper, IEducationRepo educationRepo)
        {
            _educationRepo = educationRepo;
            _majorService = majorService;
            _mapper = mapper;
            _studentService = studentService;
        }
        public async Task<Education> AcceptStudentForGraduate(int id, int studentId)
        {
            var student = _studentService.Value.GetStudentById(studentId);
            if (student == null)
            {
                throw new Exception("Student not found");
            }
            var education = await _educationRepo.FindByIdAsync(id);
            if (education == null)
            {
                throw new Exception("Education not found");
            }
            education.IsGraduated = true;
            await _educationRepo.UpdateAsync();
            return education;

        }

   

        public async Task<Education> CreateEducationForStudent(CreateEducationDto createEducationDto)
        {
            var major = _majorService.Value.GetMajorById(createEducationDto.MajorId);
            if (major == null)
            {
                throw new Exception("Major not found");
            }
            var student = _studentService.Value.GetStudentById(createEducationDto.StudentId);
            if (student == null)
            {
                throw new Exception("Student not found");
            }

            var education = _mapper.Map<Education>(createEducationDto);
            education.IsGraduated = true;
            await _educationRepo.CreateAsync(education);
            return education;
        }

        public async Task<Education> DeleteEducationForStudent(int studentId)
        {
            var education = await _educationRepo.FindByIdAsync(studentId);
            if (education == null)
            {
                throw new Exception("Education not found");
            }
            await _educationRepo.DeleteAsync(education);
            return education;
        }

        public Task<PagingResultDto<Education>> GetAllEducationOfStudentGraduated(QueryObject queryObject)
        {
            var studentsGraduated = _educationRepo.GetAllAsync(queryObject,
            where: e => e.IsGraduated == true,
            includes: e => e.Student
            );
            if (studentsGraduated == null)
            {
                throw new Exception("No student found");
            }
            return studentsGraduated;
        }

        public Task<PagingResultDto<Education>> GetAllEducationOfStudentNotGraduated(QueryObject queryObject)
        {
            var studentsGraduated = _educationRepo.GetAllAsync(queryObject,
            where: e => e.IsGraduated == false,
            includes: e => e.Student
            );
            if (studentsGraduated == null)
            {
                throw new Exception("No student found");
            }
            return studentsGraduated;
        }

        public async Task<Education> GetEducationByStudent(int studentId)
        {
            var student = _studentService.Value.GetStudentById(studentId);
            if (student == null)
            {
                throw new Exception("Student not found");
            }
            var education = await _educationRepo.findOneByConditionAsync(
                e => e.StudentId == studentId && e.IsGraduated == true
            );
            if (education == null)
            {
                throw new Exception("No education found");
            }
            return education;
        }

        public async Task<Education> UpdateEducationForStudent(UpdateEducationDto updateEducationDto, int id)
        {
            var student = _studentService.Value.GetStudentById(updateEducationDto.StudentId);
            if (student == null)
            {
                throw new Exception("Student not found");
            }
            var major = _majorService.Value.GetMajorById(updateEducationDto.MajorId);
            if (major == null)
            {
                throw new Exception("Major not found");
            }
            var education = await _educationRepo.FindByIdAsync(id);
            if (education == null)
            {
                throw new Exception("Education not found");
            }

            _mapper.Map(updateEducationDto, education);
            await _educationRepo.UpdateAsync();
            return education;
        }

    }
}