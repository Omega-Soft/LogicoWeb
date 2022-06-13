using Contracts;
using Entities.Models;

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
                .ToList();
        }

        public IEnumerable<Object> GetAllBonReceptionLot(int IdLot)
        {
            return GetAll()
                .Where(x => x.IdLot == IdLot)
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

        public _0301BonReceptionMp GetBonReceptionById(int Id)
        {
            return GetByCondition(x => x.IdBr.Equals(Id)).FirstOrDefault();
        }
    }
}



