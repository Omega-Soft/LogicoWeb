using Entities.Models;

namespace Contracts
{
    public interface IProvenanceRepository : IRepositoryBase<_0300Provenance>
    {
        public IEnumerable<Object> GetAllProvenances();

        public _0300Provenance GetProvenanceById(int Id);
    }
}
