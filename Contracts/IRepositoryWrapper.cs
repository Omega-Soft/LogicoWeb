namespace Contracts
{
    public interface IRepositoryWrapper
    {
        IUserRepository User { get; }
        IGroupRepository Group { get; }
        IRoleRepository Role { get; }
        IPageRepository Page { get; }
        IArticleRepository Article { get; }
        IFournisseurRepository Fournisseur { get; }
        IMouleRepository Moule { get; }
        IQualiteRepository Qualite { get; }
        IBateauRepository Bateau { get; }
        ICamionRepository Camion { get; }
        ITransporteurRepository Transporteur { get; }
        IProvenanceRepository Provenance { get; }
        IOrigineRepository Origine { get; }
        IBonReceptionMpREpository BonReceptionMp { get; }
        ILotMpRepository LotMp { get; }

        void Save();
    }
}