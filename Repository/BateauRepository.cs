using Contracts;
using Entities.Models;

namespace Repository
{
    public class BateauRepository : RepositoryBase<_0300Bateau>, IBateauRepository
    {
        public BateauRepository(LogicoWebContext repositoryContext)
            : base(repositoryContext)
        {
        }

        public IEnumerable<Object> GetAllBateaux()
        {
            return GetAll()
                .Select(x => new
                {
                    x.IdBateau,
                   Code= x.CodeBateau,
                   Nom=x.NomBateau,
                   Type= x.IdTypeBateauNavigation.Designation
                    
                })
                .ToList();
        }

        public _0300Bateau GetBateauById(int Id)
        {
            return GetByCondition(x => x.IdBateau.Equals(Id)).FirstOrDefault();
        }



    }
}
