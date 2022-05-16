using Entities.Models;

namespace Contracts
{
    public interface ILotMpRepository : IRepositoryBase<_0301LotMp>
    {
        public IEnumerable<Object> GetAllLotMPs();

        public _0301LotMp GetLotMPById(int Id);
    }
}

