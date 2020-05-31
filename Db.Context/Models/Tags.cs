using System;
using System.Collections.Generic;

namespace Db.Context.Models
{
    public partial class Tags
    {
        public Tags()
        {
            PostTags = new HashSet<PostTags>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<PostTags> PostTags { get; set; }
    }
}
