using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Entity;
using api.Utils;

namespace api.Dto.Subject
{
    public class SubjectDto
    {
        public int Id { get; set; }   
        public string SubjectName { get; set; }
        public int Credit { get; set; }
        public int AcademicYear { get; set; }
        public TermEnum Term { get; set; }       
        public virtual ICollection<StudentSubject> StudentSubjects { get; set; }
    }
}