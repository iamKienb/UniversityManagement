using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dto;
using api.Dto.Major;
using api.Entity;
using api.Utils;

namespace api.Services
{
    public interface IMajorService
    {
        Task<PagingResultDto<Major>> GetAllMajor(QueryObject queryObject);
        Task<PagingResultDto<Major>> GetAllStudentOfMajor(QueryObject queryObject);
        Task<Major> CreateMajor(CreateMajorDto createMajorDto);

        Task<Major> GetMajorById(int id);

        Task<Major> UpdateMajor(int id, UpdateMajorDto updateMajorDto);

        Task<Major> DeleteMajor(int id);


    }
}