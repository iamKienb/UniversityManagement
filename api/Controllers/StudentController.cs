using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dto.Student;
using api.Entity;
using api.ExceptionHandler;
using api.Services;
using api.Utils;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/student")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _studentService;
        private readonly IMapper _mapper;
        private readonly TokenUtil _tokenUtil;

        private readonly IStudentSubjectService _studentSubjectService;
        public StudentController(IStudentService studentService, IMapper mapper, TokenUtil tokenUtil, IStudentSubjectService studentSubjectService)
        {
            _studentService = studentService;
            _mapper = mapper;
            _tokenUtil = tokenUtil;
            _studentSubjectService = studentSubjectService;
        }


        [HttpPost("signup")]
        public async Task<IActionResult> SignUp([FromBody] CreateStudentDto createStudentDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var data = await _studentService.SignUp(createStudentDto);
                return Ok(_mapper.Map<StudentDto>(data));

            }
            catch (Exception e)
            {
                return StatusCode(500, new { message = $"Co loi xay ra: {e.Message}" });
            }
        }


        [HttpGet , Authorize(Roles = ("1, 2"))]
        public async Task<IActionResult> GetAllStudent([FromQuery] QueryObject queryObject)
        {
            try
            {
                var data = await _studentService.GetAllStudents(queryObject);
                return Ok(_mapper.Map<List<StudentDto>>(data));

            }
            catch (Exception e)
            {
                return StatusCode(500, new { message = $"Co loi xay ra: {e.Message}" });
            }

        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var data = await _studentService.Login(loginDto);
                return Ok(data);
            }
            catch (Exception e)
            {
                return StatusCode(500, new { message = $"Co loi xay ra: {e.Message}" });
            }

        }
        [HttpPut("{id}"), Authorize(Roles = ("1, 2, 3"))]
        public async Task<IActionResult> UpdateStudent([FromRoute] int id, [FromBody] UpdateStudentDto updateStudentDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var studentIdClaim = int.Parse(_tokenUtil.GetClaimByType(User, Constant.StudentId).Value);
                if (studentIdClaim != id)
                {
                    return Unauthorized("Bạn không có quyền cập nhật thông tin của sinh viên khác.");
                }
                var data = await _studentService.UpdateStudent(id, updateStudentDto);
                return Ok(_mapper.Map<StudentDto>(data));

            }
            catch (Exception e)
            {
                return StatusCode(500, new { message = $"Co loi xay ra: {e.Message}" });
            }

        }
        [HttpGet("{id}")]
        public async Task<IActionResult> FindStudentById([FromRoute] int id)
        {
            try
            {
                var data = await _studentService.GetStudentById(id);
                return Ok(_mapper.Map<StudentDto>(data));
            }
            catch (Exception e)
            {
                return StatusCode(500, new { message = $"Co loi xay ra: {e.Message}" });
            }
        }
        [HttpDelete("{id}"), Authorize(Roles = ("2, 3"))]
        public async Task<IActionResult> DeleteStudentById([FromRoute] int id)
        {
            try
            {
                var data = await _studentService.DeleteStudent(id);
                return Ok("Delete successfully " + data);

            }
            catch (Exception e)
            {
                return StatusCode(500, new { message = $"Co loi xay ra: {e.Message}" });
            }

        }
        [HttpPost("dk-subject"), Authorize(Roles = ("1, 2, 3"))]
        public async Task<IActionResult> CreateStudentSubject([FromBody] int SubjectId)
        {
            try
            {
                var studentId = int.Parse(_tokenUtil.GetClaimByType(User, Constant.StudentId).Value);
                var data = await _studentSubjectService.CreateStudentSubject(studentId, SubjectId);
                return Ok(data);

            }
            catch (Exception e)
            {
                return StatusCode(500, new { message = $"Co loi xay ra: {e.Message}" });
            }
        }

        [HttpDelete("delete-subject"), Authorize(Roles = ("1, 2, 3"))]
        public async Task<IActionResult> DeleteStudentSubject()
        {
            {
                try
                {
                    var studentId = int.Parse(_tokenUtil.GetClaimByType(User, Constant.StudentId).Value);
                    var data = await _studentSubjectService.DeleteStudentSubject(studentId);
                    return Ok(data);
                }
                catch (Exception e)
                {
                    return StatusCode(500, new { message = $"Co loi xay ra: {e.Message}" });
                }

            }
        }

        [HttpGet("get-subject"), Authorize(Roles = ("1, 2, 3"))]
        public async Task<IActionResult> GetSubjectOfStudent(QueryObject queryObject)
        {

            {
                try
                {
                    var studentId = int.Parse(_tokenUtil.GetClaimByType(User, Constant.StudentId).Value);
                    var data = await _studentSubjectService.GetSubjectOfStudent(queryObject, studentId);
                    return Ok(data);
                }
                catch (Exception e)
                {
                    return StatusCode(500, new { message = $"Co loi xay ra: {e.Message}" });
                }

            }

        }

    }
}