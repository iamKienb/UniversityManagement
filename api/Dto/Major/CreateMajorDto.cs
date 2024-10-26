using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dto.Major
{
    public class CreateMajorDto
    {
        public string SubjectCode { get; set; }
        public string MajorName { get; set; }
    }
}