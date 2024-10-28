using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dto.ExamSchedule;
using api.Repository;
using api.Services;
using api.Utils;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/examSchedule")]
    [ApiController]
    public class ExamScheduleController : ControllerBase
    {
        private readonly IExampleScheduleService _exampleScheduleService;
        private readonly IMapper _mapper;

        private readonly TokenUtil _tokenUtil;
        public ExamScheduleController(IExampleScheduleService exampleScheduleService, IMapper mapper, TokenUtil tokenUtil)
        {
            _exampleScheduleService = exampleScheduleService;
            _mapper = mapper;
            _tokenUtil = tokenUtil;
        }
        [HttpPost, Authorize(Roles = ("2, 3"))]
        public async Task<IActionResult> CreateExampleSchedule([FromBody] CreateExampleScheduleDto createExampleScheduleDto)
        {
            try
            {
                var data = await _exampleScheduleService.CreateExampleSchedule(createExampleScheduleDto);
                return Ok(data);
            }
            catch (Exception e)
            {
                return StatusCode(500, new { message = $"Co loi xay ra: {e.Message}" });
            }
        }

        [HttpPut, Authorize(Roles = ("2, 3"))]
        public async Task<IActionResult> UpdateExampleSchedule([FromBody]UpdateExampleScheduleDto updateExampleScheduleDto, [FromRoute]int id)
        {
            try
            {
                var data = await _exampleScheduleService.UpdateExampleSchedule(updateExampleScheduleDto, id);
                return Ok(data);
            }
            catch (Exception e)
            {
                return StatusCode(500, new { message = $"Co loi xay ra: {e.Message}" });
            }
        }

        [HttpDelete, Authorize(Roles = ("2, 3"))]
        public async Task<IActionResult> DeleteExampleSchedule([FromRoute]int id)
        {
            try
            {
                var data = await _exampleScheduleService.DeleteExampleSchedule(id);
                return Ok(data);

            }
            catch (Exception e)
            {
                return StatusCode(500, new { message = $"Co loi xay ra: {e.Message}" });
            }
        }

        [HttpGet, Authorize(Roles = ("2, 3"))]
        public async Task<IActionResult> GetAllExampleSchedules([FromQuery]QueryObject queryObject)
        {
            try
            {
                var data = await _exampleScheduleService.GetAllExampleSchedules(queryObject);
                return Ok(data);

            }
            catch (Exception e)
            {
                return StatusCode(500, new { message = $"Co loi xay ra: {e.Message}" });
            }
        }

        [HttpGet("student"), Authorize(Roles = ("1, 2, 3"))]
        public async Task<IActionResult> GetExampleScheduleOfStudent()
        {
            try
            {
                var studentId = int.Parse(_tokenUtil.GetClaimByType(User, Constant.StudentId).Value);
                var data = await _exampleScheduleService.GetExampleScheduleOfStudent(studentId);
                return Ok(data);
            }
            catch (Exception e)
            {
                return StatusCode(500, new { message = $"Co loi xay ra: {e.Message}" });
            }
        }
    }
}