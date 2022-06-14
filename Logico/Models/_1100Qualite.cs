using System;
using System.Collections.Generic;

namespace Logico.Models
{
    public partial class _1100Qualite
    {
        public _1100Qualite()
        {
            _0301DetailsReceptionMps = new HashSet<_0301DetailsReceptionMp>();
        }

        public int IdQualite { get; set; }
        public string? CodeQualite { get; set; }
        public string? Designation { get; set; }

        public virtual ICollection<_0301DetailsReceptionMp> _0301DetailsReceptionMps { get; set; }
    }
}
