using Entities.Models;

namespace Contracts
{
    public interface IFournisseurRepository : IRepositoryBase<_0400Fournisseur>
    {
        public IEnumerable<Object> GetAllFournisseurs();

        public _0400Fournisseur GetFournisseurById(int Id);
    }
    
   
}
