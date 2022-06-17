using System;
using System.Collections.Generic;

namespace Entities.Models
{
    public partial class _0301DetailsReceptionMp
    {
        public int IdDetailsBr { get; set; }
        public float? QteRecue { get; set; }
        public float? QtePayee { get; set; }
        public float? Pu { get; set; }
        public double? Eventre { get; set; }
        public double? Histamine { get; set; }
        public double? MntAchat { get; set; }
        public int? IdBr { get; set; }
        public int? IdArticle { get; set; }
        public int? IdMoule { get; set; }
        public int? IdQualite { get; set; }

        public virtual _0400Article? IdArticleNavigation { get; set; }
        public virtual _0301BonReceptionMp? IdBrNavigation { get; set; }
        public virtual _0600Moule? IdMouleNavigation { get; set; }
        public virtual _1100Qualite? IdQualiteNavigation { get; set; }
    }
}
