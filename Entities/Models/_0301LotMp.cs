using System;
using System.Collections.Generic;

namespace Entities.Models
{
    public partial class _0301LotMp
    {
        public _0301LotMp()
        {
            _0301BonReceptionMps = new HashSet<_0301BonReceptionMp>();
        }

        public int IdLot { get; set; }
        public string? CodeLot { get; set; }
        public DateTime? Journee { get; set; }

        public virtual ICollection<_0301BonReceptionMp> _0301BonReceptionMps { get; set; }
    }
}
