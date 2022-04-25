namespace Contracts
{
    public interface IRepositoryWrapper
    {
        IArticleRepository Article { get; }
        IFournisseurRepository Fournisseur { get; }
        IMouleRepository Moule { get; }
        IUserRepository User { get; }

        void Save();
    }
}