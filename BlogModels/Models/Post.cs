using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogModels.Models
{
    public class Post
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Content { get; set; } = null!;

        // Navigation property for the one-to-many relationship
        public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();
    }
}
