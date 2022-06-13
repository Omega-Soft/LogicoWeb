using System;
using System.Collections.Generic;

namespace Logico.Models
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
        public string? Fax { get; set; }

        public virtual ICollection<_0301BonReceptionMp> _0301BonReceptionMps { get; set; }
    }
}
