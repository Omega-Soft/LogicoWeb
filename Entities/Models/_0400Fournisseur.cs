using System;
using System.Collections.Generic;

namespace Entities.Models
{
    public partial class _0400Fournisseur
    {
        public _0400Fournisseur()
        {
            _0301BonReceptionMps = new HashSet<_0301BonReceptionMp>();
        }

        public int IdFournisseur { get; set; }
        public string? CodeFournisseur { get; set; }
        public string? RaisonSociale { get; set; }
        public string? Email { get; set; }
        public string? Adresse { get; set; }
        public string? Tel { get; set; }
        public DateTime? DateAnouveau { get; set; }
        public string? Fax { get; set; }
        public string? Site { get; set; }
        public string? CodePostal { get; set; }
        public string? Ville { get; set; }
        public bool? IsFrsMp { get; set; }
        public bool? IsFrsPf { get; set; }
        public bool? IsFrsCharges { get; set; }

        public virtual ICollection<_0301BonReceptionMp> _0301BonReceptionMps { get; set; }
    }
}
