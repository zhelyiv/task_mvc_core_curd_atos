using System;
using System.Collections.Generic;

namespace Db.Context.Models
{
    public partial class Posts
    {
        public Posts()
        {
            Comments = new HashSet<Comments>();
            PostTags = new HashSet<PostTags>();
        }

        public int Id { get; set; }
        public string Contents { get; set; }
        public int BlogId { get; set; }

        public virtual Blogs Blog { get; set; }
        public virtual ICollection<Comments> Comments { get; set; }
        public virtual ICollection<PostTags> PostTags { get; set; }
    }
}
