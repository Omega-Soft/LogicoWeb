using System;
using System.Collections.Generic;

namespace Entities.Models
{
    public partial class _0301BonReceptionMp
    {
        public _0301BonReceptionMp()
        {
            _0301DetailsReceptionMps = new HashSet<_0301DetailsReceptionMp>();
        }

        public int IdBr { get; set; }
        public string? CodeBr { get; set; }
        public float? Brute { get; set; }
        public int? NbPalette { get; set; }
        public string? NbonPese { get; set; }
        public float? Tare { get; set; }
        public int? NbCaisse { get; set; }
        public bool? IsNegos { get; set; }
        public float? CoutAchat { get; set; }
        public float? PrixAchat { get; set; }
        public float? Montant { get; set; }
        public DateTime? DateBr { get; set; }
        public bool? Validee { get; set; }
        public bool? Prevalidee { get; set; }
        public int? IdLot { get; set; }
        public int? IdTransporteur { get; set; }
        public int? IdCamion { get; set; }
        public int? IdProvenance { get; set; }
        public int? IdOrigine { get; set; }
        public int? IdBateau { get; set; }
        public int? IdFournisseur { get; set; }
        public bool? Deleted { get; set; }

        public virtual _0300Bateau? IdBateauNavigation { get; set; }
        public virtual _0700Camion? IdCamionNavigation { get; set; }
        public virtual _0400Fournisseur? IdFournisseurNavigation { get; set; }
        public virtual _0301LotMp? IdLotNavigation { get; set; }
        public virtual _0300Origine? IdOrigineNavigation { get; set; }
        public virtual _0300Provenance? IdProvenanceNavigation { get; set; }
        public virtual _0700Transporteur? IdTransporteurNavigation { get; set; }
        public virtual ICollection<_0301DetailsReceptionMp> _0301DetailsReceptionMps { get; set; }
    }
}
