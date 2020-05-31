using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace ViewModels
{
    public class BlogViewModel
    {
        [DisplayName("Blog ID")]
        public int Id { get; set; }

        [DisplayName("OwnerId")] 
        public int OwnerUserId { get; set; }

        [DisplayName("Owner")]
        public string OwnerUserName { get; set; }

        [DisplayName("Posts")]
        public int PostsCount { get; set; }
    }
}
