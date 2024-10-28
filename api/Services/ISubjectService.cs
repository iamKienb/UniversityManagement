using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dto;
using api.Dto.Subject;
using api.Entity;
using api.Utils;

namespace api.Services
{
    public interface ISubjectService
    {
        Task <Subject> GetSubjectById(int id);

        Task <PagingResultDto<SubjectDto>> GetAllSubject(QueryObject queryObject);

        Task <Subject> CreateSubject(CreateSubjectDto createSubjectDto);

        Task <Subject> UpdateSubject(UpdateSubjectDto updateSubjectDto, int id);

        Task<Subject> DeleteSubject(int id);
    }
}