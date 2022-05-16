using System;
using System.Collections.Generic;

namespace Entities.Models
{
    public partial class _0700Camion
    {
        public _0700Camion()
        {
            _0301BonReceptionMps = new HashSet<_0301BonReceptionMp>();
        }

        public int IdCamion { get; set; }
        public string? Matricule { get; set; }
        public int? IdTransporteur { get; set; }

        public virtual _0700Transporteur? IdTransporteurNavigation { get; set; }
        public virtual ICollection<_0301BonReceptionMp> _0301BonReceptionMps { get; set; }
    }
}
