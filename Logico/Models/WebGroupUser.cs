using System;
using System.Collections.Generic;

namespace Logico.Models
{
    public partial class WebGroupUser
    {
        public WebGroupUser()
        {
            WebRoles = new HashSet<WebRole>();
            WebUsers = new HashSet<WebUser>();
        }

        public int IdGroup { get; set; }
        public string? CodeGroup { get; set; }
        public string? Designation { get; set; }
        public bool? Deleted { get; set; }

        public virtual ICollection<WebRole> WebRoles { get; set; }
        public virtual ICollection<WebUser> WebUsers { get; set; }
    }
}
