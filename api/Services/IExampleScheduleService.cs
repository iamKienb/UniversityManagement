using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dto.ExamSchedule;
using api.Utils;

namespace api.Services
{
    public interface IExampleScheduleService
    {
        Task CreateExampleSchedule(CreateExampleScheduleDto createExampleScheduleDto);

        Task DeleteExampleSchedule(int id);

        Task GetAllExampleSchedules(QueryObject queryObject);

        Task UpdateExampleSchedule(UpdateExampleScheduleDto updateExampleScheduleDto, int id);


        Task GetExampleScheduleOfStudent(int studentId);
    }
    
}