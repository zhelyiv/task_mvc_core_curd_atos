using System;
using System.Collections.Generic;

namespace Db.Context.Models
{
    public partial class Blogs
    {
        public Blogs()
        {
            Posts = new HashSet<Posts>();
            UserBlogs = new HashSet<UserBlogs>();
        }

        public int Id { get; set; }
        public int OwnerUserId { get; set; }

        public virtual User OwnerUser { get; set; }
        public virtual ICollection<Posts> Posts { get; set; }
        public virtual ICollection<UserBlogs> UserBlogs { get; set; }
    }
}
