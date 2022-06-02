using Contracts;
using Entities.Models;

namespace Repository
{
    public class RoleRepository : RepositoryBase<WebRole>, IRoleRepository
    {
        public RoleRepository(LogicoWebContext repositoryContext)
           : base(repositoryContext)
        {
        }
        public IEnumerable<Object> GetAllRoles()
        {
            return GetAll()
                .Select(x => new
                {
                    x.IdRole,
                    x.IdGroup,
                    x.IdPage,
                    x.Read,
                    x.Add,
                    x.Update,
                    x.Delete
                })
                .ToList();
        }

        public WebRole GetRoleById(int idRole)
        {
#pragma warning disable CS8603 // Possible null reference return.
            return GetByCondition(x => x.IdRole.Equals(idRole)).FirstOrDefault();
#pragma warning restore CS8603 // Possible null reference return.
        }

        public IEnumerable<object> GetRoleByGroup(int idGroup)
        {
            return GetByCondition(x => x.IdGroup == idGroup && x.Read == true).Select(x => new
            {
                page = x.IdPageNavigation.CodePage,
                x.Add,
                x.Update,
                x.Delete
            }).ToList();
        }
    }
}