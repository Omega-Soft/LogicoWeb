using Entities.Models;

namespace Contracts
{
    public interface IPageRepository : IRepositoryBase<WebPage>
    {
        public IEnumerable<Object> GetAllPages();
        public WebPage GetPageById(int idPage);
    }
}
