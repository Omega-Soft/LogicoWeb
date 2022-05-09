using Contracts;
using Entities.Models;

namespace Repository
{
    public class TransporteurRepository : RepositoryBase<_0700Transporteur>, ITransporteurRepository
    {
        public TransporteurRepository(LogicoWebContext repositoryContext)
            : base(repositoryContext)
        {
        }

        public IEnumerable<Object> GetAllTransporteurs()
        {
            return GetAll()
                .Select(x => new
                {
                   x.IdTransporteur,
                   x.RaisonSociale,
                   x.CodeTransporteur,
                   Adresse= x.Adresse,
                   Téléphone=x.Tel
                })
                .ToList();
        }

        public _0700Transporteur GetTransporteurById(int Id)
        {
            return GetByCondition(x => x.IdTransporteur.Equals(Id)).FirstOrDefault();
        }



    }
}