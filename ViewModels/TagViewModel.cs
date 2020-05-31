using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace ViewModels
{
    public class TagViewModel
    {
        [DisplayName("Tag ID")]
        public int Id { get; set; }

        [DisplayName("Tag Name")]
        public string Name { get; set; }

        public string DisplayText
        {
            get
            {
                return $"{Id} - {Name}";
            }
        }
    }
}
