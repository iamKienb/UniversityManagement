using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.ExceptionHandler
{
    public class ConflictException : Exception
    {
        public ConflictException(String Message) : base(Message)
        {

        }
    }
}