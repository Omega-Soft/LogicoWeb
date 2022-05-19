using Entities.Models;

namespace Contracts
{
    public interface IUserRepository : IRepositoryBase<WebUser>
    {
        public IEnumerable<Object> GetAllUsers();
        public WebUser GetUserById(int idUser);
    }
}
