using System;
using System.Collections.Generic;

namespace Entities.Models
{
    public partial class _0700Transporteur
    {
        public _0700Transporteur()
        {
            _0700Camions = new HashSet<_0700Camion>();
        }

        public int IdTransporteur { get; set; }
        public string? CodeTransporteur { get; set; }
        public string? RaisonSociale { get; set; }
        public string? Adresse { get; set; }
        public string? Tel { get; set; }

        public virtual ICollection<_0700Camion> _0700Camions { get; set; }
    }
}
