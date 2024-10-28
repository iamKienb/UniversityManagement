using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace api.Entity
{
    public class ExamSchedule
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int Id { get; set; }
        public DateTime ExamDate { get; set; }
        public string Room { get; set; }
        public string TimeToStart { get; set; }

        public virtual ICollection<ExamScheduleSubject> ExamScheduleSubjects { get; set; }
        public virtual ICollection<ExamScheduleStudent> ExamScheduleStudent { get; set; }
    }
}