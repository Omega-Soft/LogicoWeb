using Entities.Models;

namespace Contracts
{
    public interface ITransporteurRepository : IRepositoryBase<_0700Transporteur>
    {
        public IEnumerable<Object> GetAllTransporteurs();

        public _0700Transporteur GetTransporteurById(int Id);
    }
}
