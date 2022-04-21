namespace Contracts
{
    public interface IRepositoryWrapper
    {
        IArticleRepository Article { get; }

        void Save();
    }
}