using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public class BonReceptionMpREpository : RepositoryBase<_0301BonReceptionMp>, IBonReceptionMpREpository
    {
 public BonReceptionMpREpository(LogicoWebContext repositoryContext)
       : base(repositoryContext)
        {
        }

        public IEnumerable<Object> GetAllBonReceptions()
        {
            return GetAll()
                .Where(x => (x.Deleted == false || x.Deleted == null))
                .Select(x => new
                {
                    x.IdLot,
                    x.IdBr,
                    x.CodeBr,
                    NumBonPese = x.NbonPese,
                    x.IdFournisseur,
                    x.DateBr,
                    x.IdCamion,
                    x.IdProvenance,
                    x.Tare,
                    x.Brute,
                    x.NbPalette,
                    x.IdTransporteur,
                    x.NbCaisse,
                    x.IsNegos,
                    x.IdOrigine,
                    x.Montant,
                    x.Prevalidee,
                    x.Validee,
                    Articles = x._0301DetailsReceptionMps.Select(y => new
                    {
                        Article = y.IdArticleNavigation.Designation,
                        Moule = y.IdMouleNavigation.Designation,
                        Qualite = y.IdQualiteNavigation.Designation,
                        y.QteRecue,
                        y.QtePayee,
                        PrixUnitaire = y.Pu,
                        MontantAchat = y.QtePayee * y.Pu,
                        y.Eventre,
                        y.Histamine,
                    }),
                })
                .ToList();
        }

        public IEnumerable<Object> GetAllBonReceptionLot(int IdLot)
        {
            return GetAll()
                .Where(x => x.IdLot == IdLot && (x.Deleted == false || x.Deleted == null))
                .Select(x => new
                {
                    x.IdLot,
                    x.IdBr,
                    x.CodeBr,
                    NumBonPese = x.NbonPese,
                    x.IdFournisseur,
                    x.DateBr,
                    x.IdCamion,
                    x.IdProvenance,
                    x.IdOrigine,
                    x.Montant,
                    x.Prevalidee,
                    x.Validee,
                    Articles = x._0301DetailsReceptionMps.Select(y => new
                    {
                        y.IdDetailsBr,
                        y.IdArticle,
                        y.IdMoule,
                        y.IdQualite,
                        y.QteRecue,
                        y.QtePayee,
                        PrixUnitaire = y.Pu,
                        y.Eventre,
                        y.Histamine,
                        MontantAchat = y.MntAchat
                    }),
                })
                .ToList();
        }

        public Object GetBonReceptionById(int Id)
        {
#pragma warning disable CS8603 // Possible null reference return.
            return GetByCondition(x => x.IdBr.Equals(Id) && (x.Deleted == false || x.Deleted == null))
                .Select(x => new
                {
                    x.IdLot,
                    x.IdBr,
                    x.CodeBr,
                    NumBonPese = x.NbonPese,
                    x.IdFournisseur,
                    x.DateBr,
                    x.IdCamion,
                    x.IdProvenance,
                    x.Tare,
                    x.Brute,
                    x.NbPalette,
                    x.IdTransporteur,
                    x.NbCaisse,
                    x.IsNegos,
                    x.IdOrigine,
                    x.Montant,
                    x.Prevalidee,
                    x.Validee,
                    Articles = x._0301DetailsReceptionMps.Select(y => new
                    {
                        y.IdDetailsBr,
                        y.IdArticle,
                        y.IdMoule,
                        y.IdQualite,
                        y.QteRecue,
                        y.QtePayee,
                        PrixUnitaire = y.Pu,
                        y.Eventre,
                        y.Histamine,
                        MontantAchat = y.MntAchat
                    }),
                })
                .FirstOrDefault();
#pragma warning restore CS8603 // Possible null reference return.
        }
    }
}



