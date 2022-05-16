using System;
using System.Collections.Generic;

namespace Entities.Models
{
    public partial class _0400Article
    {
        public _0400Article()
        {
            _0301DetailsReceptionMps = new HashSet<_0301DetailsReceptionMp>();
        }

        public int IdArticle { get; set; }
        public string? CodeArticle { get; set; }
        public string? Designation { get; set; }

        public virtual ICollection<_0301DetailsReceptionMp> _0301DetailsReceptionMps { get; set; }
    }
}
