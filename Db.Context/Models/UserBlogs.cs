using System;
using System.Collections.Generic;

namespace Db.Context.Models
{
    public partial class UserBlogs
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int BlogId { get; set; }

        public virtual Blogs Blog { get; set; }
        public virtual User User { get; set; }
    }
}
