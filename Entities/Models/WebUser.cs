using System;
using System.Collections.Generic;

namespace Entities.Models
{
    public partial class WebUser
    {
        public int IdUser { get; set; }
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string? Nom { get; set; }
        public string? Prenom { get; set; }
        public int? IdGroup { get; set; }

        public virtual WebGroupUser? IdGroupNavigation { get; set; }
    }
}
