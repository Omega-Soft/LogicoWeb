using Contracts;
using Entities.Models;

namespace Repository
{
    public class CamionRepository : RepositoryBase<_0700Camion>, ICamionRepository
    {
        public CamionRepository(LogicoWebContext repositoryContext)
            : base(repositoryContext)
        {
        }

        public IEnumerable<Object> GetAllCamions()
        {
            return GetAll().ToList();
        }

        public _0700Camion GetCamionById(int Id)
        {
            return GetByCondition(x => x.IdCamion.Equals(Id)).FirstOrDefault();
        }



    }
}