using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dto;
using api.Dto.Education;
using api.Entity;
using api.Utils;

namespace api.Services.ServicesImp
{
    public class EducationService : IEducationService
    {
        public Task<Education> AcceptStudentForGraduate(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Education> CreateEducationForStudent(CreateEducationDto createEducationDto)
        {
            throw new NotImplementedException();
        }

        public Task DeleteEducationForStudent(int studentId)
        {
            throw new NotImplementedException();
        }

        public Task<PagingResultDto<Education>> GetAllEducationOfStudentGraduated(QueryObject queryObject)
        {
            throw new NotImplementedException();
        }

        public Task<PagingResultDto<Education>> GetAllEducationOfStudentNotGraduated(QueryObject queryObject)
        {
            throw new NotImplementedException();
        }

        public Task<Education> GetEducationByStudent(int studentId)
        {
            throw new NotImplementedException();
        }

        public Task<Education> UpdateEducationForStudent(UpdateEducationDto updateEducationDto, int id)
        {
            throw new NotImplementedException();
        }
    }
}