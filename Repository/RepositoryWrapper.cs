using Contracts;
using Entities.Models;

namespace Repository
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private LogicoWebContext _context;
        private IArticleRepository _article;
        private IFournisseurRepository _fournisseur;
        private IMouleRepository _moule;
        private IUserRepository _user;
        private IQualiteRepository _qualite;
        private IBateauRepository _bateau;
        private ITransporteurRepository _transporteur;
        private ICamionRepository _camion;
        private IOrigineRepository _origine;
        private IProvenanceRepository _provenance; 

        public IUserRepository User
        {
            get
            {
                if (_user == null)
                {
                    _user = new UserRepository(_context);
                }

                return _user;
            }
        }
        public IArticleRepository Article
        {
            get
            {
                if (_article == null)
                {
                    _article = new ArticleRepository(_context);
                }

                return _article;
            }
        }
        public IFournisseurRepository Fournisseur
        {
            get
            {
                if (_fournisseur == null)
                {
                    _fournisseur = new FournisseurRepository(_context);
                }

                return _fournisseur;
            }
        }
        public IMouleRepository Moule
        {
            get
            {
                if (_moule == null)
                {
                    _moule = new MouleRepository(_context);
                }

                return _moule;
            }
        }

        public IQualiteRepository Qualite
        {
            get
            {
                if (_qualite == null)
                {
                    _qualite = new QualiteRepository(_context);
                }

                return _qualite;
            }
        }
        public IBateauRepository Bateau
        {
            get
            {
                if (_bateau == null)
                {
                    _bateau = new BateauRepository(_context);
                }

                return _bateau;
            }
        }
        public ICamionRepository Camion
        {
            get
            {
                if (_camion == null)
                {
                    _camion = new CamionRepository(_context);
                }

                return _camion;
            }
        }

        public ITransporteurRepository Transporteur
        {
            get
            {
                if (_transporteur == null)
                {
                    _transporteur = new TransporteurRepository(_context);
                }

                return _transporteur;
            }
        }
        public IOrigineRepository Origine
        {
            get
            {
                if (_origine == null)
                {
                    _origine = new OriginesRepository(_context);
                }

                return _origine;
            }
        }

        public IProvenanceRepository Provenance

        {
            get
            {
                if (_provenance == null)
                {
                    _provenance = new ProvenanceRepository(_context);
                }

                return _provenance;
            }
        }

        public RepositoryWrapper(LogicoWebContext context)
        {
            _context = context;
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}