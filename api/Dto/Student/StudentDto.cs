using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dto.Student
{
    public class StudentDto
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }

        public string Gender { get; set; }  = string.Empty;

        public string Phone { get; set; }  = string.Empty;
   

        public string Avatar_url { get; set; } = string.Empty;

        public string MajorName { get; set;} = string.Empty;
    }
}