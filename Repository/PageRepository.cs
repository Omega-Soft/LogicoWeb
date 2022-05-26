using Contracts;
using Entities.Models;

namespace Repository
{
    public class PageRepository : RepositoryBase<WebPage>, IPageRepository
    {
        public PageRepository(LogicoWebContext repositoryContext)
           : base(repositoryContext)
        {
        }
        public IEnumerable<Object> GetAllPages()
        {
            return GetAll()
                .Where(p => p.Deleted != true)
                .Select(x => new
                {
                    x.IdPage,
                    x.CodePage,
                    x.Designation
                })
                .ToList();
        }

        public WebPage GetPageById(int idPage)
        {
#pragma warning disable CS8603 // Possible null reference return.
            return GetByCondition(x => x.IdPage.Equals(idPage) && x.Deleted != true).FirstOrDefault();
#pragma warning restore CS8603 // Possible null reference return.
        }
    }
}