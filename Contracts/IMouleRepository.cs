using Entities.Models;

namespace Contracts
{
    public interface IMouleRepository : IRepositoryBase<_0600Moule>
    {
        public IEnumerable<Object> GetAllMoules();

        public _0600Moule GetMouleById(int Id);
    }
}
