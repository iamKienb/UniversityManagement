using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dto;
using api.Dto.Major;
using api.Entity;
using api.ExceptionHandler;
using api.Repository;
using api.Utils;
using AutoMapper;

namespace api.Services.ServicesImp
{
    public class MajorService : IMajorService
    {
        private readonly IMajorRepo _majorRepo;
        private readonly Lazy<IStudentService> _studentService;
        private readonly IMapper _mapper;
        public MajorService(Lazy<IStudentService> studentService,IMajorRepo majorRepo,  IMapper mapper)
        {
            _majorRepo = majorRepo;
            _mapper = mapper;
            _studentService = studentService;
        } 
          
        public async Task<Major> CreateMajor(CreateMajorDto createMajorDto)
        {
            var existMajor = await _majorRepo.FindByCode(createMajorDto.SubjectCode);
            if (existMajor != null)
            {
                throw new Exception("Major already exists");
            }
            var newMajor = _mapper.Map<Major>(createMajorDto);
            await _majorRepo.CreateAsync(newMajor);
            return newMajor;

        }

        public async Task<PagingResultDto<Major>> GetAllMajor(QueryObject queryObject)
        {
            var pagingResultDto = await _majorRepo.GetAllAsync(queryObject);
            return pagingResultDto;
        }

        public async Task<PagingResultDto<Major>> GetAllStudentOfMajor(QueryObject queryObject)
        {
            var pagingResultDto = await _majorRepo.GetAllAsync(queryObject,
            where: null,
            includes: m => m.Students);
            return pagingResultDto;
        }

        public async Task<Major>  UpdateMajor(int id, UpdateMajorDto updateMajorDto){
            var existMajor = await _majorRepo.FindByIdAsync(id);
            if (existMajor == null)
            {
                throw new NotFoundException("Major not found");
            }
            _mapper.Map(updateMajorDto, existMajor);
            await _majorRepo.UpdateAsync();
            return existMajor;
        } 

        public async Task<Major> DeleteMajor(int id){ 
            var existMajor = await _majorRepo.FindByIdAsync(id);
            if (existMajor == null)
            {
                throw new NotFoundException("Major not found");
            }
            await _majorRepo.DeleteAsync(existMajor);
            return existMajor;
        }

        public async Task<Major> GetMajorById(int id){
            var major = await _majorRepo.FindByIdAsync(id);
            if (major == null)
            {
                throw new NotFoundException("Major not found");
            }
            return major;
        }
    }
}