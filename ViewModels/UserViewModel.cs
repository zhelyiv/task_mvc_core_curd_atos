using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace ViewModels
{
    public class UserViewModel
    {
        [DisplayName("User ID")]
        public int Id { get; set; }

        [DisplayName("Login Name")]
        public string Login { get; set; }

        [DisplayName("Owned Blogs")]
        public int OwnedBlogsCount { get; set; }
    }
}
