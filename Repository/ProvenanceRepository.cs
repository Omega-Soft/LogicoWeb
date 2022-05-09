using Contracts;
using Entities.Models;

namespace Repository
{
    public class ProvenanceRepository : RepositoryBase<_0300Provenance>, IProvenanceRepository
    {
        public ProvenanceRepository(LogicoWebContext repositoryContext)
            : base(repositoryContext)
        {
        }

        public IEnumerable<Object> GetAllProvenances()
        {
            return GetAll()
                .Select(x => new
                {
                    x.IdProvenance,
                    x.CodeProvenance,
                    x.Designation
                })
                .ToList();
        }

        public _0300Provenance GetProvenanceById(int Id)
        {
            return GetByCondition(x => x.IdProvenance.Equals(Id)).FirstOrDefault();
        }



    }
}