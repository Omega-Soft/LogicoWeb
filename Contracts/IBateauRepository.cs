using Entities.Models;

namespace Contracts
{
    public interface IBateauRepository : IRepositoryBase<_0300Bateau>
    {
        public IEnumerable<Object> GetAllBateaux();

        public _0300Bateau GetBateauById(int Id);

    }
}
