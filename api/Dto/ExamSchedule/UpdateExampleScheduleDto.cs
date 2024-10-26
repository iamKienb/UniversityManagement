using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dto.ExamSchedule
{
    public class UpdateExampleScheduleDto
    {
        public DateTime ExamDate { get; set; }
        public string Room { get; set; }

        public string TimeToStart { get; set; }

        public int StudentId { get; set; }
 
        public int SubjectId { get; set; }
   
    }
}