using System;
using System.Collections.Generic;

namespace Entities.Models
{
    public partial class _0700Camion
    {
        public int IdCamion { get; set; }
        public string? Matricule { get; set; }
        public int? IdTransporteur { get; set; }

        public virtual _0700Transporteur? IdTransporteurNavigation { get; set; }
    }
}
