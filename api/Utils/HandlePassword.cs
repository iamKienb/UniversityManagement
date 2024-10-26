using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Entity;
using Microsoft.AspNetCore.Identity;

namespace api.Utils
{
    public class HandlePassword
    {
        public string HashPassword(string password)
        {
            // Hash mật khẩu bằng BCrypt với mức độ bảo mật cao (work factor là 12)
            return BCrypt.Net.BCrypt.HashPassword(password, workFactor: 12);
        }

        public bool VerifyPassword(string password, string hashedPassword)
        {
            // Xác thực mật khẩu bằng cách so sánh với hashedPassword
            return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
        }
    }

}