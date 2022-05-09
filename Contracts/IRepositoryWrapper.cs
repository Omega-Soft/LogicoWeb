namespace Contracts
{
    public interface IRepositoryWrapper
    {
        IArticleRepository Article { get; }
        IFournisseurRepository Fournisseur { get; }
        IMouleRepository Moule { get; }
        IUserRepository User { get; }
        IQualiteRepository Qualite { get; }
        IBateauRepository Bateau { get; }
        ICamionRepository Camion { get; }
        ITransporteurRepository Transporteur { get; }
        IProvenanceRepository Provenance { get; }
        IOrigineRepository Origine { get; }

        void Save();
    }
}