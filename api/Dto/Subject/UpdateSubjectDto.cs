using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using api.Utils;

namespace api.Dto.Subject
{
    public class UpdateSubjectDto
    {
        public string SubjectName { get; set; }
        public int Credit { get; set; }
        public int AcademicYear { get; set; }
        public TermEnum Term { get; set; }

    }
}