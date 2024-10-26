using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using api.Entity;
using Microsoft.IdentityModel.Tokens;

namespace api.Utils
{
    public class TokenHelper
    {
        private readonly IConfiguration _config;
        public TokenHelper(IConfiguration config)
        {
            _config = config;
        }

        public string GenerateJwtToken(Student student)
        {
            // Bước 1: Tạo các claims (thông tin được mã hóa trong token)
            var claims = new List<Claim>
            {
                new Claim(Constant.StudentId, student.Id.ToString()), // student ID
                new Claim(Constant.Email, student.Email), // studentname
                new Claim(ClaimTypes.Role, ((int)student.Role).ToString()) // Vai trò của student (admin, student, etc.)
            };

            // Bước 2: Tạo khóa bí mật để ký token (lấy từ cấu hình)
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Token:SecretKey"]));

            // Bước 3: Tạo thông tin mã hóa và ký token bằng HMAC SHA512
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            // Bước 4: Tạo đối tượng JwtSecurityToken
            var token = new JwtSecurityToken(
                claims: claims, // Gán claims vào token
                expires: DateTime.Now.AddDays(7), // Token hết hạn sau 1 ngày
                signingCredentials: creds // Ký token với khóa bí mật
            );

            // Bước 5: Trả về token dưới dạng chuỗi JWT
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}