using Entities.Models;

namespace Contracts
{
    public interface IBonReceptionMpREpository : IRepositoryBase<_0301BonReceptionMp>
    {
        public IEnumerable<Object> GetAllBonReceptions();
        public IEnumerable<Object> GetAllBonReceptionLot(int IdLot);

        public _0301BonReceptionMp GetBonReceptionById(int Id);
    }
}
