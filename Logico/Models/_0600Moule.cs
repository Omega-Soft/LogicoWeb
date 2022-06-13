using System;
using System.Collections.Generic;

namespace Logico.Models
{
    public partial class _0600Moule
    {
        public _0600Moule()
        {
            _0301DetailsReceptionMps = new HashSet<_0301DetailsReceptionMp>();
        }

        public int IdMoule { get; set; }
        public string? CodeMoule { get; set; }
        public string? Designation { get; set; }

        public virtual ICollection<_0301DetailsReceptionMp> _0301DetailsReceptionMps { get; set; }
    }
}
