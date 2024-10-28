using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dto.Feeds
{
    public class CreateFeedDto
    {
        [Required(ErrorMessage = "Title is required")]
        public string Title { get; set; }

        [StringLength(int.MaxValue)]
        [Required(ErrorMessage = "Content is required")]
        public string Content { get; set; }

        public string Picture { get; set; } =  string.Empty;

    }
}