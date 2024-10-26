using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using api.ExceptionHandler;
namespace api.Utils
{
    public class TokenUtil
    {
        public Claim GetClaimByType(ClaimsPrincipal user, string type)
        {
            // Tìm claim có type khớp với tham số "type" truyền vào
            var value = user.Claims.FirstOrDefault(c => c.Type == type);
            if (value == null)
            {
                throw new BadRequestException("Token is not valid");
            }
            return value;
        }
    }
}