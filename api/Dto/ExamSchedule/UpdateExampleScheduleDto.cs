using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dto.ExamSchedule
{
    public class UpdateExampleScheduleDto
    {
        [Required(ErrorMessage = "ExamDate is required")]
        public DateTime ExamDate { get; set; }

        [Required(ErrorMessage = "Room is required")]
        public string Room { get; set; }

        [Required(ErrorMessage = "TimeToStart is required")]
        public string TimeToStart { get; set; }

        [Required(ErrorMessage = "StudentId is required")]
        public int StudentId { get; set; }

        [Required(ErrorMessage = "SubjectId is required")]
        public int SubjectId { get; set; }

    }
}