using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using api.Dto;
using api.Dto.ExamSchedule;
using api.Entity;
using api.Repository;
using api.Utils;
using AutoMapper;

namespace api.Services.ServicesImp
{
    public class ExampleScheduleService : IExampleScheduleService
    {
        private readonly IExampleScheduleRepo _exampleScheduleRepo;
        private readonly IMapper _mapper;
        private readonly Lazy<IStudentService> _studentService;
        private readonly Lazy<ISubjectService> _subjectService;

        private readonly IExamScheduleStudentRepo _examScheduleStudentRepo;
        private readonly IExamScheduleSubjectRepo _examScheduleSubjectRepo;

        public ExampleScheduleService(IExampleScheduleRepo exampleScheduleRepo, IMapper mapper, Lazy<IStudentService> studentService, Lazy<ISubjectService> subjectService, IExamScheduleStudentRepo examScheduleStudentRepo, IExamScheduleSubjectRepo examScheduleSubjectRepo)
        {
            _exampleScheduleRepo = exampleScheduleRepo;
            _mapper = mapper;
            _examScheduleStudentRepo = examScheduleStudentRepo;
            _examScheduleSubjectRepo = examScheduleSubjectRepo;
            _studentService = studentService;
            _subjectService = subjectService;
        }
        public async Task<ExamSchedule> CreateExampleSchedule(CreateExampleScheduleDto createExampleScheduleDto)
        {
            var student = _studentService.Value.GetStudentById(createExampleScheduleDto.StudentId);
            if (student == null)
            {
                throw new Exception("Student not found");
            }
            var subject = _subjectService.Value.GetSubjectById(createExampleScheduleDto.SubjectId);
            if (subject == null)
            {
                throw new Exception("Subject not found");
            }
            var examSchedule = _mapper.Map<ExamSchedule>(createExampleScheduleDto);
            var createdSchedule = await _exampleScheduleRepo.CreateAsync(examSchedule);

            var examScheduleStudent = new ExamScheduleStudent
            {
                ExamScheduleId = createdSchedule.Id,
                StudentId = student.Id
            };
            await _examScheduleStudentRepo.CreateAsync(examScheduleStudent);

            var examScheduleSubject = new ExamScheduleSubject
            {
                ExamScheduleId = createdSchedule.Id,
                SubjectId = subject.Id
            };
            await _examScheduleSubjectRepo.CreateAsync(examScheduleSubject);

            return createdSchedule;
        }

        public async Task<ExamSchedule> DeleteExampleSchedule(int id)
        {
            var schedule = await _exampleScheduleRepo.FindByIdAsync(id);
            if (schedule == null)
            {
                throw new Exception("Schedule not found");
            }
            await _exampleScheduleRepo.DeleteAsync(schedule);
            var examScheduleStudent = await _examScheduleStudentRepo.findOneByConditionAsync(
                filter: ess => ess.ExamScheduleId == id
            );
            await _examScheduleStudentRepo.DeleteAsync(examScheduleStudent);
            var examScheduleSubject = await _examScheduleSubjectRepo.findOneByConditionAsync(
                filter: ess => ess.ExamScheduleId == id
            );
            await _examScheduleSubjectRepo.DeleteAsync(examScheduleSubject);
            return schedule;
        }

        public async Task<PagingResultDto<ExamScheduleDto>> GetAllExampleSchedules(QueryObject queryObject)
        {

            var schedules = await _exampleScheduleRepo.GetAllAsync(
                queryObject,
                where: null,
                es => es.ExamScheduleStudent,
                es => es.ExamScheduleSubjects
            );
            var examScheduleDtos = _mapper.Map<List<ExamScheduleDto>>(schedules.ResultItems);

            return new PagingResultDto<ExamScheduleDto>(examScheduleDtos, schedules.TotalRecords, schedules.PageSize, schedules.CurrentPage);
        }

        public async Task<List<ExamSchedule>> GetExampleScheduleOfStudent(int studentId)
        {
            var student = _studentService.Value.GetStudentById(studentId);
            if (student == null)
            {
                throw new Exception("Student not found");
            }
            var schedules = await _examScheduleStudentRepo.findOneByConditionAsync(
                est => est.StudentId == studentId
            );
            if (schedules == null)
            {
                throw new Exception("No schedule found for this student");
            }


            var scheduleOfStudent = await _exampleScheduleRepo.GetAllAsync(
                queryObject: null,
                where: es => es.Id == schedules.ExamScheduleId,
                es => es.ExamScheduleSubjects
            );

            return scheduleOfStudent.ResultItems;

        }

        public async Task<ExamSchedule> UpdateExampleSchedule(UpdateExampleScheduleDto updateExampleScheduleDto, int id)
        {
            var existExamSchedule = await _exampleScheduleRepo.FindByIdAsync(id);

            if (existExamSchedule == null)
            {
                throw new Exception("Schedule not found");
            }
            var student = _studentService.Value.GetStudentById(updateExampleScheduleDto.StudentId);
            if (student == null)
            {
                throw new Exception("Student not found");
            }
            var subject = _subjectService.Value.GetSubjectById(updateExampleScheduleDto.SubjectId);
            if (subject == null)
            {
                throw new Exception("Subject not found");
            }

            _mapper.Map(updateExampleScheduleDto, existExamSchedule);
            await _exampleScheduleRepo.UpdateAsync();
            return existExamSchedule;


        }
    }
    
}