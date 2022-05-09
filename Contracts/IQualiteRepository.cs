using Entities.Models;

namespace Contracts
{
    public interface IQualiteRepository : IRepositoryBase<_1100Qualite>
    {
        public IEnumerable<Object> GetAllQualites();

        public _1100Qualite GetQualiteById(int Id);
    }
}
