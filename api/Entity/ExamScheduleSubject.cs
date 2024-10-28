using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace api.Entity
{
    public class ExamScheduleSubject
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int ExamScheduleId { get; set; }
        public virtual ExamSchedule ExamSchedule { get; set; }

        public int SubjectId { get; set; }
        public virtual Subject Subject { get; set; }
    }
}