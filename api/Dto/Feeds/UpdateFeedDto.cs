using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dto.Feeds
{
    public class UpdateFeedDto
    {
        public string Title { get; set; }

        [StringLength(int.MaxValue)]
        public string Content { get; set; }

        public string Picture { get; set; } = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRYqO2-ugiq4JXp-w6pbniXhJceIg8_9Bu3uA&usqp=CAU";
    }
}