using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace ViewModels
{
    public class CommentViewModel
    {
        [DisplayName("Comment ID")]
        public int Id { get; set; }

        [DisplayName("Text")]
        public string Contents { get; set; }

        [DisplayName("User ID")]
        public int UserId { get; set; }

        [DisplayName("User Login")]
        public string UserLogin { get; set; }

        [DisplayName("Post ID")]
        public int PostId { get; set; }
    }
}
