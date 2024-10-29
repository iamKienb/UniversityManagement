using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dto.Subject;
using api.Services;
using api.Utils;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/subject")]
    [ApiController]
    public class SubjectController : ControllerBase
    {
        private readonly ISubjectService _subjectService;
        private readonly IMapper _mapper;

        public SubjectController(ISubjectService subjectService, IMapper mapper)
        {
            _subjectService = subjectService;
            _mapper = mapper;
        }

        // GET: api/subject
        [HttpGet]
        public async Task<ActionResult> GetAllSubjects([FromQuery] QueryObject queryObject)
        {
            try
            {
                var data = await _subjectService.GetAllSubject(queryObject);
                return Ok(data);
            }
            catch (Exception e)
            {
                return StatusCode(500, new { message = $"Co loi xay ra: {e.Message}" });
            }
        }

        // GET: api/subject/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult> GetSubjectById([FromRoute] int id)
        {
            try
            {
                var data = await _subjectService.GetSubjectById(id);
                return Ok(data);
            }
            catch (Exception e)
            {
                return StatusCode(500, new { message = $"Co loi xay ra: {e.Message}" });
            }
        }

        // POST: api/subject
        [HttpPost , Authorize(Roles = ("2 , 3"))]
        public async Task<ActionResult> CreateSubject([FromBody] CreateSubjectDto subjectDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var data = await _subjectService.CreateSubject(subjectDto);


                return Ok(data);
            }
            catch (Exception e)
            {
                return StatusCode(500, new { message = $"Co loi xay ra: {e.Message}" });
            }
        }

        // PUT: api/subject/{id}
        [HttpPut("{id}") , Authorize(Roles = ("2 , 3"))] 
        public async Task<IActionResult> UpdateSubject([FromRoute] int id, [FromBody] UpdateSubjectDto updatedSubjectDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var data = await _subjectService.UpdateSubject(updatedSubjectDto, id);
                return Ok(data);
            }
            catch (Exception e)
            {
                return StatusCode(500, new { message = $"Co loi xay ra: {e.Message}" });
            }
        }

        // DELETE: api/subject/{id}
        [HttpDelete("{id}"), Authorize(Roles = ("2 , 3"))]
        public async Task<IActionResult> DeleteSubject([FromRoute] int id)
        {
            try
            {
                var data = await _subjectService.DeleteSubject(id);
                return Ok(data);
            }
            catch (Exception e)
            {
                return StatusCode(500, new { message = $"Co loi xay ra: {e.Message}" });
            }
        }
    }
}