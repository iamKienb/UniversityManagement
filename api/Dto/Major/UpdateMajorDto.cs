using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dto.Major
{
    public class UpdateMajorDto
    {
        [Required(ErrorMessage = "SubjectCode is required")]
        public string SubjectCode { get; set; }

        [Required(ErrorMessage = "MajorName is required")]
        public string MajorName { get; set; }
    }
}