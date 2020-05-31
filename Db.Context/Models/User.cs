using System;
using System.Collections.Generic;

namespace Db.Context.Models
{
    public partial class User
    {
        public User()
        {
            Blogs = new HashSet<Blogs>();
            Comments = new HashSet<Comments>();
            UserBlogs = new HashSet<UserBlogs>();
        }

        public int Id { get; set; }
        public string Login { get; set; }

        public virtual ICollection<Blogs> Blogs { get; set; }
        public virtual ICollection<Comments> Comments { get; set; }
        public virtual ICollection<UserBlogs> UserBlogs { get; set; }
    }
}
