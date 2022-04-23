using System;
using System.Collections.Generic;

namespace Entities.Models
{
    public partial class WebRole
    {
        public int IdRole { get; set; }
        public bool? Read { get; set; }
        public bool? Add { get; set; }
        public bool? Update { get; set; }
        public bool? Delete { get; set; }
        public int? IdGroup { get; set; }
        public int? IdPage { get; set; }

        public virtual WebGroupUser? IdGroupNavigation { get; set; }
        public virtual WebPage? IdPageNavigation { get; set; }
    }
}
