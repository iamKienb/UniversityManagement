using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dto;
using api.Dto.Education;
using api.Entity;
using api.Utils;

namespace api.Services
{
    public interface IEducationService
    {
        Task<Education> CreateEducationForStudent(CreateEducationDto createEducationDto);

        Task DeleteEducationForStudent(int studentId);

        Task<Education> UpdateEducationForStudent(UpdateEducationDto updateEducationDto, int id);

        Task<Education> GetEducationByStudent(int studentId);

        Task<Education> AcceptStudentForGraduate(int id);

        Task<PagingResultDto<Education>> GetAllEducationOfStudentNotGraduated(QueryObject queryObject);

        Task<PagingResultDto<Education>> GetAllEducationOfStudentGraduated(QueryObject queryObject);
    }
}