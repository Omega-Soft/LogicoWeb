using Contracts;
using Entities.Models;

namespace Repository
{
    public class MouleRepository : RepositoryBase<_0600Moule>, IMouleRepository
    {
        public MouleRepository(LogicoWebContext repositoryContext)
       : base(repositoryContext)
        {
        }
        public IEnumerable<Object> GetAllMoules()
        {
            return GetAll()
                .Select(x => new
                {
                    Id = x.IdMoule,
                    Code = x.CodeMoule,
                    Moule = x.Designation
                })
                .ToList();
        }

        public _0600Moule GetMouleById(int Id)
        {
            return GetByCondition(x => x.IdMoule.Equals(Id)).FirstOrDefault();
        }

    }
}



