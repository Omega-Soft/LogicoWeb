namespace Contracts
{
    public interface IRepositoryWrapper
    {
        IArticleRepository Article { get; }
        IUserRepository User { get; }

        void Save();
    }
}