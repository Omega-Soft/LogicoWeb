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
            return GetAll().ToList();
        }
            public _0400Fournisseur GetFournisseurById(int Id)
            {
                return GetByCondition(x => x.IdFournisseur.Equals(Id)).FirstOrDefault();
            }
        }

    
}