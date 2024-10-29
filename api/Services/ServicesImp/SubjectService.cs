using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dto;
using api.Dto.Subject;
using api.Entity;
using api.ExceptionHandler;
using api.Repository;
using api.Utils;
using AutoMapper;

namespace api.Services.ServicesImp
{
    public class SubjectService : ISubjectService
    {
        private readonly ISubjectRepo _subjectRepo;
        private readonly IMapper _mapper;

        public SubjectService(ISubjectRepo subjectRepo, IMapper mapper)
        {
            _mapper = mapper;
            _subjectRepo = subjectRepo;
        }
        public async Task<Subject> CreateSubject(CreateSubjectDto createSubjectDto)
        {
            var newSubject = _mapper.Map<Subject>(createSubjectDto);
            await _subjectRepo.CreateAsync(newSubject);
            return newSubject;
        }

        public async Task<Subject> DeleteSubject(int id)
        {
            var existSubject = await _subjectRepo.FindByIdAsync(id);

            if (existSubject == null)
            {
                throw new NotFoundException("Subject not found");
            }

            await _subjectRepo.DeleteAsync(existSubject);

            return existSubject;
        }

        public async Task<PagingResultDto<SubjectDto>> GetAllSubject(QueryObject queryObject)
        {
            var pagingResult = await _subjectRepo.GetAllAsync(queryObject, null, null);

            var subjectDtos = _mapper.Map<List<SubjectDto>>(pagingResult.ResultItems);
            return new PagingResultDto<SubjectDto>(subjectDtos, pagingResult.TotalRecords, pagingResult.PageSize, pagingResult.CurrentPage);
        }

        public async Task<Subject> GetSubjectById(int id)
        {
            var existSubject = await _subjectRepo.FindByIdAsync(id);

            if (existSubject == null)
            {
                throw new NotFoundException("Subject not found");
            }
            return existSubject;
        }

        public  async Task<Subject> UpdateSubject(UpdateSubjectDto updateSubjectDto, int id)
        {
            var existSubject = await _subjectRepo.FindByIdAsync(id);

            if (existSubject == null)
            {
                throw new NotFoundException("Subject not found");
            }

            _mapper.Map(updateSubjectDto, existSubject);
            await _subjectRepo.UpdateAsync();
            return existSubject;
        }

    }
}