using Entities.Models;

namespace Contracts
{
    public interface IArticleRepository : IRepositoryBase<_0400Article>
    {
        public IEnumerable<Object> GetAllArticles();

        public _0400Article GetArticleById(int Id);

    }
}
