using Contracts;
using Entities.Models;

namespace Repository
{
    public class GroupRepository : RepositoryBase<WebGroupUser>, IGroupRepository
    {
        public GroupRepository(LogicoWebContext repositoryContext)
           : base(repositoryContext)
        {
        }
        public IEnumerable<Object> GetAllGroups()
        {
            return GetAll()
                .Where(x => x.Deleted != true)
                .Select(x => new
                {
                    x.IdGroup,
                    x.CodeGroup,
                    x.Designation,
                    users = x.WebUsers,
                    pages = x.WebRoles.Where(x => x.Read == true).ToList().Count > 0 
                            ? x.WebRoles.Where(x => x.Read == true).Select(x => new
                            {
                                x.IdPage,
                                x.IdPageNavigation.CodePage,
                                x.IdPageNavigation.Designation,
                                x.Add,
                                x.Update,
                                x.Delete
                            }).ToList()
                            : null
                })
                .ToList();
        }

        public WebGroupUser GetGroupById(int idGroup)
        {
#pragma warning disable CS8603 // Possible null reference return.
            return GetByCondition(x => x.IdGroup.Equals(idGroup) && x.Deleted != true).FirstOrDefault();
#pragma warning restore CS8603 // Possible null reference return.
        }
    }
}
