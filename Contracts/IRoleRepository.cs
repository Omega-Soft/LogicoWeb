using Entities.Models;

namespace Contracts
{
    public interface IRoleRepository : IRepositoryBase<WebRole>
    {
        public IEnumerable<Object> GetAllRoles();
        public WebRole GetRoleById(int idRole);
    }
}