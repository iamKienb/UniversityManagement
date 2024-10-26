using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using api.Utils;

namespace api.Entity
{
    public class Subject
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }   
        public string SubjectName { get; set; }
        public int Credit { get; set; }
        public int AcademicYear { get; set; }
        public TermEnum Term { get; set; }       
        public virtual ICollection<StudentSubject> StudentSubjects { get; set; }

    }
}