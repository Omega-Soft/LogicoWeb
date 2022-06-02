using System;
using System.Collections.Generic;

namespace Logico.Models
{
    public partial class _0300Origine
    {
        public _0300Origine()
        {
            _0301BonReceptionMps = new HashSet<_0301BonReceptionMp>();
        }

        public int IdOrigine { get; set; }
        public string? CodeOrigine { get; set; }
        public string? Designation { get; set; }

        public virtual ICollection<_0301BonReceptionMp> _0301BonReceptionMps { get; set; }
    }
}
