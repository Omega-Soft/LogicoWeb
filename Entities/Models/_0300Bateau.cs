using System;
using System.Collections.Generic;

namespace Entities.Models
{
    public partial class _0300Bateau
    {
        public _0300Bateau()
        {
            _0301BonReceptionMps = new HashSet<_0301BonReceptionMp>();
        }

        public int IdBateau { get; set; }
        public string CodeBateau { get; set; } = null!;
        public string? NomBateau { get; set; }
        public int? IdTypeBateau { get; set; }

        public virtual _0300TypeBateau? IdTypeBateauNavigation { get; set; }
        public virtual ICollection<_0301BonReceptionMp> _0301BonReceptionMps { get; set; }
    }
}
