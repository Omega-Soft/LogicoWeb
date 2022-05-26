using System;
using System.Collections.Generic;

namespace Entities.Models
{
    public partial class WebPage
    {
        public WebPage()
        {
            WebRoles = new HashSet<WebRole>();
        }

        public int IdPage { get; set; }
        public string? CodePage { get; set; }
        public string? Designation { get; set; }
        public bool? Deleted { get; set; }

        public virtual ICollection<WebRole> WebRoles { get; set; }
    }
}
