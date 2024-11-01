using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace api.Entity
{
    public class NewsFeed
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Title { get; set; }

        [StringLength(int.MaxValue)]
        public string Content { get; set; }

        public string Picture { get; set; } =  string.Empty;

        public bool IsPublished { get; set; } = false;

        public int StudentId { get; set; }
        public virtual Student Student { get; set; }

        public DateTime CreatedAt  { get; set; } = DateTime.UtcNow;

        
    }
}