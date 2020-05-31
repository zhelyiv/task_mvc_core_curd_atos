using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace ViewModels
{
    public class PostViewModel
    {
        [DisplayName("Post ID")]
        public int Id { get; set; }

        [DisplayName("Text")]
        public string Contents { get; set; }

        [DisplayName("Blog")]
        public int BlogId { get; set; }

        [DisplayName("Tags")]
        public List<string> Tags { get; set; }

        public string DisplayText
        {
            get
            {
                int len = Math.Min(25, (Contents?.Length ?? 0));
                return $"{Id} - {Contents?.Substring(0, len)}...";
            }
        }

    }
}
