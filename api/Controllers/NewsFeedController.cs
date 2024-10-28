using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dto;
using api.Dto.Feeds;
using api.Dto.Major;
using api.Services;
using api.Utils;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/newsFeed")]
    [ApiController]
    public class NewsFeedController : ControllerBase
    {
        private readonly INewsFeedService _newsFeedService;

        private readonly IMapper _mapper;

        private readonly TokenUtil _tokenUtil;

        public NewsFeedController(INewsFeedService newsFeedService, TokenUtil tokenUtil, IMapper mapper)
        {
            _newsFeedService = newsFeedService;
            _mapper = mapper;
            _tokenUtil = tokenUtil;
        }

        [HttpPost("createFeed"), Authorize(Roles = ("1 , 2, 3"))]
        public async Task<IActionResult> CreateNewsFeed([FromBody] CreateFeedDto createFeedDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                var studentId = int.Parse(_tokenUtil.GetClaimByType(User, Constant.StudentId).Value);
                var newsFeed = await _newsFeedService.CreateNewsFeed(createFeedDto, studentId);
                return Ok(_mapper.Map<NewFeedDto>(newsFeed));
            }
            catch (Exception e)
            {
                return StatusCode(500, new { message = $"Co loi xay ra: {e.Message}" });
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllNewsFeeds([FromQuery] QueryObject queryObject)
        {
            try
            {
                var data = await _newsFeedService.GetAllNewsFeeds(queryObject);
                var feedDtos = _mapper.Map<List<NewFeedDto>>(data.ResultItems);
                return Ok(new PagingResultDto<NewFeedDto>(feedDtos, data.TotalRecords, data.PageSize, data.CurrentPage));
            }
            catch (Exception e)
            {
                return StatusCode(500, new { message = $"Co loi xay ra: {e.Message}" });
            }

        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetNewsFeedById([FromQuery] int id)
        {
            try
            {
                var feed = await _newsFeedService.GetNewsFeedById(id);
                return Ok(_mapper.Map<NewFeedDto>(feed));
            }
            catch (Exception e)
            {
                return StatusCode(500, new { message = $"Co loi xay ra: {e.Message}" });
            }
        }

        [HttpPut("{id}"), Authorize(Roles = ("1 , 2, 3"))]
        public async Task<IActionResult> UpdateNewsFeed([FromBody] UpdateFeedDto updateFeedDto, [FromRoute] int id)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                var studentId = int.Parse(_tokenUtil.GetClaimByType(User, Constant.StudentId).Value);
                if (studentId != id)
                {
                    return Forbid("Unauthorized");
                }
                var feed = await _newsFeedService.UpdateNewsFeed(updateFeedDto, id);
                return Ok(_mapper.Map<NewFeedDto>(feed));
            }
            catch (Exception e)
            {
                return StatusCode(500, new { message = $"Co loi xay ra: {e.Message}" });
            }
        }

        [HttpDelete("{id}"), Authorize(Roles = ("1 , 2, 3"))]
        public async Task<IActionResult> DeleteNewsFeed([FromRoute] int id)
        {
            try
            {
                var studentId = int.Parse(_tokenUtil.GetClaimByType(User, Constant.StudentId).Value);
                if (studentId != id)
                {
                    return Forbid("Unauthorized");
                }
                var feed = _newsFeedService.DeleteNewsFeed(id);
                return Ok("Delete Success");
            }
            catch (Exception e)
            {
                return StatusCode(500, new { message = $"Co loi xay ra: {e.Message}" });
            }
        }

        [HttpGet("getPost-student/{studentId}")]
        public async Task<IActionResult> GetFeedsByStudentId([FromRoute]int studentId)
        {
            try
            {   
                var feeds = await _newsFeedService.GetFeedsByStudentId(studentId);
                return Ok(_mapper.Map<List<NewFeedDto>>(feeds));
            }
            catch (Exception e)
            {
                return StatusCode(500, new { message = $"Co loi xay ra: {e.Message}" });
            }
        }

        [HttpGet("get-draft-post")]
        public async Task<IActionResult> getAllDraftPost([FromQuery] QueryObject queryObject)
        {
            try
            {
                var data = await _newsFeedService.GetDraftFeeds(queryObject);
                var feedDtos = _mapper.Map<List<NewFeedDto>>(data.ResultItems);
                return Ok(new PagingResultDto<NewFeedDto>(feedDtos, data.TotalRecords, data.PageSize, data.CurrentPage));
            }
            catch (Exception e)
            {
                return StatusCode(500, new { message = $"Co loi xay ra: {e.Message}" });
            }
        }

        [HttpPut("public/{id}"), Authorize(Roles = ("1 , 2, 3")) ]
        public async Task<IActionResult> publishPost([FromRoute]int id)
        {
            try
            {
                var data = await _newsFeedService.PublishFeed(id);
                return Ok(data);
            }
            catch (Exception e)
            {
                return StatusCode(500, new { message = $"Co loi xay ra: {e.Message}" });
            }
        }

        [HttpPut("unPublic/{id}"), ]
        public async Task<IActionResult> unpublishPost(int id)
        {
            try
            {
                var data = await _newsFeedService.PublishFeed(id);
                return Ok(data);
            }
            catch (Exception e)
            {
                return StatusCode(500, new { message = $"Co loi xay ra: {e.Message}" });
            }
        }
    }
}