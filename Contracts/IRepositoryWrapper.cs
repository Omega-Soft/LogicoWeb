namespace Contracts
{
    public interface IRepositoryWrapper
    {
        IArticleRepository Article { get; }
        IFournisseurRepository Fournisseur { get; }
        IUserRepository User { get; }

        void Save();
    }
}