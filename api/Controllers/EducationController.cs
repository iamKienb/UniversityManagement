using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dto.Education;
using api.Services;
using api.Utils;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/education")]
    [ApiController]
    public class EducationController : ControllerBase
    {
        private readonly IEducationService _educationService;
        private readonly IMapper _mapper;
        public EducationController(IEducationService educationService, IMapper mapper)
        {
            _educationService = educationService;
            _mapper = mapper;
        }
        [HttpPost("create"), Authorize(Roles = ("2, 3"))]
        public async Task<IActionResult> CreateEducationForStudent([FromBody] CreateEducationDto createEducationDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var data = await _educationService.CreateEducationForStudent(createEducationDto);
                return Ok(_mapper.Map<EducationDto>(data));
            }
            catch (Exception e)
            {
                return StatusCode(500, new { message = $"Co loi xay ra: {e.Message}" });
            }
        }

        [HttpDelete(), Authorize(Roles = ("2, 3"))]
        public async Task<IActionResult> DeleteEducationForStudent([FromBody] int studentId)
        {

            try
            {
                var data = await _educationService.DeleteEducationForStudent(studentId);
                return Ok(_mapper.Map<EducationDto>(data));
            }
            catch (Exception e)
            {
                return StatusCode(500, new { message = $"Co loi xay ra: {e.Message}" });
            }

        }
        [HttpPut("{id}"), Authorize(Roles = ("2, 3"))]
        public async Task<IActionResult> UpdateEducationForStudent([FromBody] UpdateEducationDto updateEducationDto, [FromRoute] int id)
        {

            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var data = await _educationService.UpdateEducationForStudent(updateEducationDto, id);
                return Ok(_mapper.Map<EducationDto>(data));

            }
            catch (Exception e)
            {
                return StatusCode(500, new { message = $"Co loi xay ra: {e.Message}" });
            }

        }

        [HttpPut("accept/{id}"), Authorize(Roles = ("2, 3"))]
        public async Task<IActionResult> AcceptStudentForGraduate([FromRoute] int id, [FromBody] int studentId)
        {

            try
            {

                var data = await _educationService.AcceptStudentForGraduate(id, studentId);
                return Ok(_mapper.Map<EducationDto>(data));
            }
            catch (Exception e)
            {
                return StatusCode(500, new { message = $"Co loi xay ra: {e.Message}" });
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetAllEducationOfStudentGraduated([FromRoute] QueryObject queryObject)
        {
            try
            {
                var data = await _educationService.GetAllEducationOfStudentGraduated(queryObject);
                return Ok(data);

            }
            catch (Exception e)
            {
                return StatusCode(500, new { message = $"Co loi xay ra: {e.Message}" });
            }
        }

        [HttpGet("student-not-graduated"), Authorize(Roles = ("2, 3"))]
        public async Task<IActionResult> GetAllEducationOfStudentNotGraduated([FromQuery] QueryObject queryObject)
        {
            try
            {
                var data = await _educationService.GetAllEducationOfStudentNotGraduated(queryObject);
                return Ok(data);
            }
            catch (Exception e)
            {
                return StatusCode(500, new { message = $"Co loi xay ra: {e.Message}" });
            }
        }
        [HttpGet("student"), Authorize(Roles = ("1, 2, 3"))]
        public async Task<IActionResult> GetEducationByStudent([FromBody] int studentId)
        {
            try
            {
                var data = await _educationService.GetEducationByStudent(studentId);
                return Ok(_mapper.Map<EducationDto>(data));
            }
            catch (Exception e)
            {
                return StatusCode(500, new { message = $"Co loi xay ra: {e.Message}" });
            }
        }
    }
}