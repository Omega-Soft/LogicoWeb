using System;
using System.Collections.Generic;

namespace Logico.Models
{
    public partial class _0300Provenance
    {
        public _0300Provenance()
        {
            _0301BonReceptionMps = new HashSet<_0301BonReceptionMp>();
        }

        public int IdProvenance { get; set; }
        public string? CodeProvenance { get; set; }
        public string? Designation { get; set; }

        public virtual ICollection<_0301BonReceptionMp> _0301BonReceptionMps { get; set; }
    }
}
