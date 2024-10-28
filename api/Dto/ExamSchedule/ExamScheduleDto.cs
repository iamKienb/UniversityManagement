using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dto.ExamSchedule
{
    public class ExamScheduleDto
    {
        public int Id { get; set; }
        public DateTime ExamDate { get; set; }
        public string Room { get; set; }

        public string TimeToStart { get; set; }
        public virtual ICollection<api.Entity.Student> Students { get; set; }

        public virtual ICollection<api.Entity.Student> Subjects { get; set; }
    }
}