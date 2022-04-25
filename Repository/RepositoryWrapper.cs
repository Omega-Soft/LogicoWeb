using Contracts;
using Entities.Models;

namespace Repository
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private LogicoWebContext _context;
        private IArticleRepository _article;
        private IFournisseurRepository _fournisseur;
        private IUserRepository _user;

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