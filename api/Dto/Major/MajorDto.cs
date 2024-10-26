using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Entity;

namespace api.Dto.Major
{
    public class MajorDto
    {
        public int Id { get; set; }
        public string SubjectCode { get; set; }
        public string MajorName { get; set; }

        public virtual ICollection<api.Entity.Student> Students { get; set;}
    }
}