using Entities.Models;

namespace Contracts
{
    public interface IBonReceptionMpREpository : IRepositoryBase<_0301BonReceptionMp>
    {
        public IEnumerable<Object> GetAllBonReceptions();

        public _0301BonReceptionMp GetBonReceptionById(int Id);
    }
}
