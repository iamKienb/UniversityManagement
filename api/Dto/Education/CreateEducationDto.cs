using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dto.Education
{
    public class CreateEducationDto
    {
        [Required(ErrorMessage = "School is required")]
        public string School { get; set; }

        [Required(ErrorMessage = "Degree is required")]
        public string Degree { get; set; }

        [Column(TypeName = "numeric(1,2)")]
        [Required(ErrorMessage = "GPA is required")]
        public decimal GPA { get; set; }

        [Required(ErrorMessage = "StartYear is required")]
        public DateTime StartYear { get; set; }
        

        [Required(ErrorMessage = "EndYear is required")]
        public DateTime? EndYear { get; set; }


        [Required(ErrorMessage = "StudentId is required")]
        public int StudentId { get; set; }


        [Required(ErrorMessage = "MajorId is required")]
        public int MajorId { get; set; }
    }
}