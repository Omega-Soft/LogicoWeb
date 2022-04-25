using Contracts;
using Entities.Models;

namespace Repository
{
    public class FournisseurRepository : RepositoryBase<_0400Fournisseur>, IFournisseurRepository
    {
        public FournisseurRepository(LogicoWebContext repositoryContext)
       : base(repositoryContext)
        {
        }

        public IEnumerable<Object> GetAllFournisseurs()
        {
            return GetAll()
                .Select(x => new
                {
                    Id = x.IdFournisseur,
                    Code = x.CodeFournisseur,
                    RaisonSociale = x.RaisonSociale,
                    Adresse = x.Adresse,
                    Email = x.Email,
                    Tel = x.Tel


                })
                .ToList();
        }
            public _0400Fournisseur GetFournisseurById(int Id)
            {
                return GetByCondition(x => x.IdFournisseur.Equals(Id)).FirstOrDefault();
            }
        }

    
}