using Entities.Models;

namespace Contracts
{
    public interface ICamionRepository : IRepositoryBase<_0700Camion>
    {
        public IEnumerable<Object> GetAllCamions();

        public _0700Camion GetCamionById(int Id);
    }
}
