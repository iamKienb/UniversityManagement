using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dto;
using api.Dto.ExamSchedule;
using api.Entity;
using api.Utils;

namespace api.Services
{
    public interface IExampleScheduleService
    {
        Task<ExamSchedule>  CreateExampleSchedule(CreateExampleScheduleDto createExampleScheduleDto);

        Task<ExamSchedule> DeleteExampleSchedule(int id);

        Task <PagingResultDto<ExamScheduleDto>>  GetAllExampleSchedules(QueryObject queryObject);

        Task<ExamSchedule> UpdateExampleSchedule(UpdateExampleScheduleDto updateExampleScheduleDto, int id);


        Task <List<ExamSchedule>> GetExampleScheduleOfStudent(int studentId);
    }
    
}