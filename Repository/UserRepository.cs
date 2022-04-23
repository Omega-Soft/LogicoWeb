using Contracts;
using Entities.Models;

namespace Repository
{
    public class UserRepository : RepositoryBase<WebUser>, IUserRepository
    {
        public UserRepository(LogicoWebContext repositoryContext)
           : base(repositoryContext)
        {
        }
    }
}
