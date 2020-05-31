using System.ComponentModel;

namespace ViewModels
{
    public class PostTagViewModel
    {
        [DisplayName("Post ID")]
        public int PostId { get; set; }

        [DisplayName("Tag ID")]
        public int TagId { get; set; }

        [DisplayName("Tag Name")]
        public string TagName { get; set; }
    }
}
