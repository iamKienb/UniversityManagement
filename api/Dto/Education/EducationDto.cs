using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dto.Education
{
    public class EducationDto
    {
        public int Id { get; set; }

        public string StudentName { get; set; }

        public string School { get; set; }
        public string MajorName { get; set; }

        public string Degree { get; set; }

        [Column(TypeName = "numeric(1,2)")]
        public decimal GPA  { get; set; }
        public DateTime StartYear { get; set; }
        public DateTime? EndYear { get; set; }
    }
}