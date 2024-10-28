using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using api.Utils;

namespace api.Dto.Subject
{
    public class UpdateSubjectDto
    {
        [Required(ErrorMessage = "SubjectName is required")]
        public string SubjectName { get; set; }

        [Required(ErrorMessage = "Credit is required")]
        public int Credit { get; set; }

        [Required(ErrorMessage = "AcademicYear is required")]
        public int AcademicYear { get; set; }

        [Required(ErrorMessage = "Term is required")]
        public TermEnum Term { get; set; }

    }
}