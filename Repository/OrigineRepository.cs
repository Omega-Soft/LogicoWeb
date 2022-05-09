using Contracts;
using Entities.Models;

namespace Repository
{
    public class OriginesRepository : RepositoryBase<_0300Origine>, IOrigineRepository
    {
        public OriginesRepository(LogicoWebContext repositoryContext)
            : base(repositoryContext)
        {
        }

        public IEnumerable<Object> GetAllOrigines()
        {
            return GetAll()
                .Select(x => new
                {
                    x.IdOrigine,
                    x.CodeOrigine,
                    x.Designation
                })
                .ToList();
        }

        public _0300Origine GetOrigineById(int Id)
        {
            return GetByCondition(x => x.IdOrigine.Equals(Id)).FirstOrDefault();
        }



    }
}