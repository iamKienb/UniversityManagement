using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using api.Utils;

namespace api.Dto.Student
{
    public class CreateStudentDto
    {
        [Required(ErrorMessage = "FullName is required")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Gender is required")]
        public string Gender { get; set; }

        public string Phone { get; set; }  = string.Empty;

        public string Avatar_url { get; set; } = string.Empty;
        
        public int MajorId { get; set; }

    }
}