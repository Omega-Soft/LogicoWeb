using System;
using System.Collections.Generic;

namespace Entities.Models
{
    public partial class _0300Bateau
    {
        public int IdBateau { get; set; }
        public string CodeBateau { get; set; } = null!;
        public string? NomBateau { get; set; }
        public int? IdTypeBateau { get; set; }

        public virtual _0300TypeBateau? IdTypeBateauNavigation { get; set; }
    }
}
