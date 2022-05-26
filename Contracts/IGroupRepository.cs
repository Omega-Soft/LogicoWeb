using Entities.Models;

namespace Contracts
{
    public interface IGroupRepository : IRepositoryBase<WebGroupUser>
    {
        public IEnumerable<Object> GetAllGroups();
        public WebGroupUser GetGroupById(int idGroup);
    }
}
