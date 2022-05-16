using Contracts;
using Entities.Models;

namespace Repository 
{
    public class LotMpRepository : RepositoryBase<_0301LotMp>, ILotMpRepository
    {
    public LotMpRepository(LogicoWebContext repositoryContext)
          : base(repositoryContext)
    {
    }
        public IEnumerable<Object> GetAllLotMPs()
    {
        return GetAll().ToList();
    }
    public _0301LotMp GetLotMPById(int Id)
    {
        return GetByCondition(x => x.IdLot.Equals(Id)).FirstOrDefault();
    }
    }
}


