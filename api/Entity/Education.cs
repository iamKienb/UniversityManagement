using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace api.Entity
{
    public class Education
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string School { get; set; }

        public string Degree { get; set; }


        [Column(TypeName = "numeric(1,2)")]
        public decimal GPA  { get; set; }
        public DateTime StartYear { get; set; }
        public DateTime? EndYear { get; set; }
        public int StudentId { get; set; }

        public virtual Student Student { get; set; }

        public int MajorId { get; set; }
        public virtual Major Major { get; set; }

        public bool IsGraduated { get; set; } = false;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;


    }
}