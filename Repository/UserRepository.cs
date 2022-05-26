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
        public IEnumerable<Object> GetAllUsers()
        {
            return GetAll()
                .Select(x => new
                {
                    x.IdUser,
                    x.Username,
                    x.Password,
                    x.Nom,
                    x.Prenom,
                    x.IdGroup
                })
                .ToList();
        }

        public WebUser GetUserById(int idUser)
        {
#pragma warning disable CS8603 // Possible null reference return.
            return GetByCondition(x => x.IdUser.Equals(idUser)).FirstOrDefault();
#pragma warning restore CS8603 // Possible null reference return.
        }
    }
}
