using System;
using System.Collections.Generic;

namespace Entities.Models
{
    public partial class _0300TypeBateau
    {
        public _0300TypeBateau()
        {
            _0300Bateaus = new HashSet<_0300Bateau>();
        }

        public int IdType { get; set; }
        public string? CodeType { get; set; }
        public string? Designation { get; set; }

        public virtual ICollection<_0300Bateau> _0300Bateaus { get; set; }
    }
}
