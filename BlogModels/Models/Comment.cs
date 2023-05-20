using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogModels.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string Content { get; set; } = null!;

        // Foreign key for the one-to-many relationship
        public int PostId { get; set; }

    }
}
