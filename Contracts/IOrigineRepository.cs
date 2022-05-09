using Entities.Models;

namespace Contracts
{
    public interface IOrigineRepository : IRepositoryBase<_0300Origine>
    {
        public IEnumerable<Object> GetAllOrigines();

        public _0300Origine GetOrigineById(int Id);
    }
}
