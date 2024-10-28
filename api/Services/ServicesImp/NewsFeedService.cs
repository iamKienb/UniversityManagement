using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dto;
using api.Dto.Feeds;
using api.Entity;
using api.Repository;
using api.Utils;
using AutoMapper;

namespace api.Services.ServicesImp
{
    public class NewsFeedService : INewsFeedService
    {

        private readonly IMapper _mapper;
        private readonly INewsFeedRepo _newsFeedRepo;

        private readonly Lazy<IStudentService> _studentService;

        public NewsFeedService(IMapper mapper, INewsFeedRepo newsFeedRepo, Lazy<IStudentService> studentService)
        {
            _mapper = mapper;
            _newsFeedRepo = newsFeedRepo;
            _studentService = studentService;
        }
        public async Task<NewsFeed> CreateNewsFeed(CreateFeedDto createFeedDto, int StudentId)
        {
            var existStudent = await _studentService.Value.GetStudentById(StudentId);
            if (existStudent == null)
            {
                throw new Exception("Student not found");
            }
            var newsFeed = _mapper.Map<NewsFeed>(createFeedDto);
            newsFeed.StudentId = existStudent.Id;
            await _newsFeedRepo.CreateAsync(newsFeed);
            return newsFeed;
        }

        public async Task<NewsFeed> DeleteNewsFeed(int id)
        {
            var existFeed = await _newsFeedRepo.FindByIdAsync(id);
            if (existFeed == null)
            {
                throw new Exception("Feed not found");
            }
            await _newsFeedRepo.DeleteAsync(existFeed); 
            return existFeed;
        }

        public async Task<PagingResultDto<NewsFeed>> GetAllNewsFeeds(QueryObject queryObject)
        {
            var pagingResult = await _newsFeedRepo.GetAllAsync(queryObject, r => r.IsPublished == true, f => f.Student);
            
            return pagingResult;
        }

        public async Task<PagingResultDto<NewsFeed>> GetDraftFeeds(QueryObject queryObject)
        {
            var getAllDraftPost = await _newsFeedRepo.GetAllAsync(queryObject,
            where: r => r.IsPublished == false, 
            includes: f => f.Student);

            return new PagingResultDto<NewsFeed>(getAllDraftPost.ResultItems, getAllDraftPost.TotalRecords, getAllDraftPost.PageSize, getAllDraftPost.CurrentPage);
        }

        public Task<List<NewsFeed>> GetFeedsByStudentId(int studentId)
        {
            var existingStudent = _studentService.Value.GetStudentById(studentId);
            if (existingStudent == null)
            {
                throw new Exception("Student not found");
            }
            var feedList = _newsFeedRepo.GetNewsFeedByStudentIdAsync(studentId);
            return feedList;
        }

        public Task<NewsFeed> GetNewsFeedById(int id)
        {
            var feed = _newsFeedRepo.FindByIdAsync(id);
            if (feed == null){
                throw new Exception("Feed not found");
            }
            return feed;
        }

        public async Task<NewsFeed> PublishFeed(int id)
        {
            var existFeed = await _newsFeedRepo.FindByIdAsync(id);
            if (existFeed == null)
            {
                throw new Exception("Feed not found");
            }
            existFeed.IsPublished = true;
            await _newsFeedRepo.UpdateAsync();
            return existFeed;
        }

        public async Task<NewsFeed> UnpublishFeed(int id)
        {
            var existFeed = await _newsFeedRepo.FindByIdAsync(id);
            if (existFeed == null)
            {
                throw new Exception("Feed not found");
            }
            existFeed.IsPublished = false;
            await _newsFeedRepo.UpdateAsync();
            return existFeed;
        }

        public async Task<NewsFeed> UpdateNewsFeed(UpdateFeedDto updateFeedDto, int id)
        {
            var existFeed = await _newsFeedRepo.FindByIdAsync(id);
            if (existFeed == null)
            {
                throw new Exception("Feed not found");
            }
            _mapper.Map(updateFeedDto, existFeed);
            await _newsFeedRepo.UpdateAsync();
            return existFeed;
        }
    }
}