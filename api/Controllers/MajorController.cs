using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dto.Major;
using api.Services;
using api.Utils;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/major")]
    [ApiController]
    public class MajorController : ControllerBase
    {
        private readonly IMajorService _majorService;
        private readonly IMapper _mapper;

        public MajorController(IMajorService majorService, IMapper mapper)
        {
            _majorService = majorService;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllMajor([FromQuery] QueryObject queryObject)
        {
            try
            {
                var majors = await _majorService.GetAllMajor(queryObject);
                return Ok(_mapper.Map<List<MajorDto>>(majors));

            }
            catch (Exception e)
            {
                return StatusCode(500, new { message = $"Co loi xay ra: {e.Message}" });
            }

        }

        [HttpPost("create-major"), Authorize(Roles = ("1, 2"))]
        public async Task<IActionResult> CreateMajor([FromBody] CreateMajorDto createMajorDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                var major = await _majorService.CreateMajor(createMajorDto);
                return Ok(_mapper.Map<MajorDto>(major));

            }
            catch (Exception e)
            {
                return StatusCode(500, new { message = $"Co loi xay ra: {e.Message}" });
            }

        }

        [HttpPut("{id}"), Authorize(Roles = ("1, 2"))]
        public async Task<IActionResult> UpdateMajor([FromRoute] int id, [FromBody] UpdateMajorDto updateMajorDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                var major = await _majorService.UpdateMajor(id, updateMajorDto);
                return Ok(_mapper.Map<MajorDto>(major));
            }
            catch (Exception e)
            {
                return StatusCode(500, new { message = $"Co loi xay ra: {e.Message}" });
            }

        }

        [HttpDelete("{id}"), Authorize(Roles = ("1, 2"))]
        public async Task<IActionResult> DeleteMajor([FromRoute] int id)
        {
            try
            {
                var major = await _majorService.DeleteMajor(id);
                return Ok("DELETE SUCCESSFULLY" + major);
            }
            catch (Exception e)
            {
                return StatusCode(500, new { message = $"Co loi xay ra: {e.Message}" });
            }

        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetMajorById([FromRoute] int id)
        {
            try
            {
                var major = await _majorService.GetMajorById(id);
                return Ok(_mapper.Map<MajorDto>(major));
            }
            catch (Exception e)
            {
                return StatusCode(500, new { message = $"Co loi xay ra: {e.Message}" });
            }

        }


        [HttpGet("students")]
        public async Task<IActionResult> GetAllStudentOfMajor([FromQuery] QueryObject queryObject){
            try{
                var majors = await _majorService.GetAllStudentOfMajor(queryObject);
                return Ok(_mapper.Map<MajorDto>(majors));

            }catch (Exception e){
                return StatusCode(500, new { message = $"Co loi xay ra: {e.Message}" });
            }
        }
    }
}