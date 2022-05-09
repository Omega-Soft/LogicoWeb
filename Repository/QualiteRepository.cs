using Contracts;
using Entities.Models;

namespace Repository
{
    public class QualiteRepository : RepositoryBase<_1100Qualite>, IQualiteRepository
    {
        public QualiteRepository(LogicoWebContext repositoryContext)
            : base(repositoryContext)
        {
        }

        public IEnumerable<Object> GetAllQualites()
        {
            return GetAll()
                .Select(x => new
                {
                    x.IdQualite,
                    x.CodeQualite,
                    x.Designation
                })
                .ToList();
        }

        public _1100Qualite GetQualiteById(int Id)
        {
            return GetByCondition(x => x.IdQualite.Equals(Id)).FirstOrDefault();
        }



    }
}
