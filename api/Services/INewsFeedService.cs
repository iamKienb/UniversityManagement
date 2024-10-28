using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dto;
using api.Dto.Feeds;
using api.Entity;
using api.Utils;

namespace api.Services
{
    public interface INewsFeedService
    {
        Task<NewsFeed> CreateNewsFeed(CreateFeedDto createFeedDto, int StudentId);
        Task<PagingResultDto<NewsFeed>> GetAllNewsFeeds(QueryObject queryObject);

        Task<NewsFeed> GetNewsFeedById(int id);

        Task<NewsFeed> UpdateNewsFeed(UpdateFeedDto updateFeedDto, int id);

        Task<NewsFeed> DeleteNewsFeed(int id);

        Task<List<NewsFeed>> GetFeedsByStudentId(int studentId);

        Task<PagingResultDto<NewsFeed>> GetDraftFeeds(QueryObject queryObject);

        Task<NewsFeed> PublishFeed(int id);

        Task<NewsFeed> UnpublishFeed(int id);
    }
}