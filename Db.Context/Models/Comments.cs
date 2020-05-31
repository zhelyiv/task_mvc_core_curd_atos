using System;
using System.Collections.Generic;

namespace Db.Context.Models
{
    public partial class Comments
    {
        public int Id { get; set; }
        public string Contents { get; set; }
        public int UserId { get; set; }
        public int PostId { get; set; }

        public virtual Posts Post { get; set; }
        public virtual User User { get; set; }
    }
}
