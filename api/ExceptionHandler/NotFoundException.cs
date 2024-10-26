using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.ExceptionHandler
{
    public class NotFoundException: Exception
    {
        public NotFoundException(String message) : base(message)
        {

        }
    }
}