using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Entities.Models
{
    public partial class LogicoWebContext : DbContext
    {
        public LogicoWebContext()
        {
        }

        public LogicoWebContext(DbContextOptions<LogicoWebContext> options)
            : base(options)
        {
        }

        public virtual DbSet<WebGroupUser> WebGroupUsers { get; set; } = null!;
        public virtual DbSet<WebPage> WebPages { get; set; } = null!;
        public virtual DbSet<WebRole> WebRoles { get; set; } = null!;
        public virtual DbSet<WebUser> WebUsers { get; set; } = null!;
        public virtual DbSet<_0300Bateau> _0300Bateaus { get; set; } = null!;
        public virtual DbSet<_0300Origine> _0300Origines { get; set; } = null!;
        public virtual DbSet<_0300Provenance> _0300Provenances { get; set; } = null!;
        public virtual DbSet<_0300TypeBateau> _0300TypeBateaus { get; set; } = null!;
        public virtual DbSet<_0301BonReceptionMp> _0301BonReceptionMps { get; set; } = null!;
        public virtual DbSet<_0301DetailsReceptionMp> _0301DetailsReceptionMps { get; set; } = null!;
        public virtual DbSet<_0301LotMp> _0301LotMps { get; set; } = null!;
        public virtual DbSet<_0400Article> _0400Articles { get; set; } = null!;
        public virtual DbSet<_0400Fournisseur> _0400Fournisseurs { get; set; } = null!;
        public virtual DbSet<_0600Moule> _0600Moules { get; set; } = null!;
        public virtual DbSet<_0700Camion> _0700Camions { get; set; } = null!;
        public virtual DbSet<_0700Transporteur> _0700Transporteurs { get; set; } = null!;
        public virtual DbSet<_1100Qualite> _1100Qualites { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=192.168.100.160;Database=LogicoWeb;User ID=sa;Password=Omega@@123");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<WebGroupUser>(entity =>
            {
                entity.HasKey(e => e.IdGroup);

                entity.ToTable("Web_GroupUsers");

                entity.HasIndex(e => e.CodeGroup, "IX_Web_GroupUsers")
                    .IsUnique();

                entity.Property(e => e.IdGroup).HasColumnName("idGroup");

                entity.Property(e => e.CodeGroup)
                    .HasMaxLength(50)
                    .HasColumnName("codeGroup");

                entity.Property(e => e.Deleted)
                    .HasColumnName("deleted")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Designation)
                    .HasMaxLength(50)
                    .HasColumnName("designation");
            });

            modelBuilder.Entity<WebPage>(entity =>
            {
                entity.HasKey(e => e.IdPage);

                entity.ToTable("Web_Page");

                entity.HasIndex(e => e.CodePage, "IX_Web_Page")
                    .IsUnique();

                entity.Property(e => e.IdPage).HasColumnName("idPage");

                entity.Property(e => e.CodePage)
                    .HasMaxLength(50)
                    .HasColumnName("codePage");

                entity.Property(e => e.Deleted)
                    .HasColumnName("deleted")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Designation)
                    .HasMaxLength(50)
                    .HasColumnName("designation");
            });

            modelBuilder.Entity<WebRole>(entity =>
            {
                entity.HasKey(e => e.IdRole);

                entity.ToTable("Web_Role");

                entity.Property(e => e.IdRole).HasColumnName("idRole");

                entity.Property(e => e.Add).HasColumnName("add");

                entity.Property(e => e.Delete).HasColumnName("delete");

                entity.Property(e => e.IdGroup).HasColumnName("idGroup");

                entity.Property(e => e.IdPage).HasColumnName("idPage");

                entity.Property(e => e.Read).HasColumnName("read");

                entity.Property(e => e.Update).HasColumnName("update");

                entity.HasOne(d => d.IdGroupNavigation)
                    .WithMany(p => p.WebRoles)
                    .HasForeignKey(d => d.IdGroup)
                    .HasConstraintName("FK_Web_Role_Web_GroupUsers");

                entity.HasOne(d => d.IdPageNavigation)
                    .WithMany(p => p.WebRoles)
                    .HasForeignKey(d => d.IdPage)
                    .HasConstraintName("FK_Web_Role_Web_Page");
            });

            modelBuilder.Entity<WebUser>(entity =>
            {
                entity.HasKey(e => e.IdUser);

                entity.ToTable("Web_User");

                entity.HasIndex(e => e.Username, "IX_Web_User")
                    .IsUnique();

                entity.Property(e => e.IdUser).HasColumnName("idUser");

                entity.Property(e => e.Deleted)
                    .HasColumnName("deleted")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.IdGroup).HasColumnName("idGroup");

                entity.Property(e => e.Nom)
                    .HasMaxLength(50)
                    .HasColumnName("nom");

                entity.Property(e => e.Password)
                    .HasMaxLength(50)
                    .HasColumnName("password");

                entity.Property(e => e.Prenom)
                    .HasMaxLength(50)
                    .HasColumnName("prenom");

                entity.Property(e => e.Username)
                    .HasMaxLength(50)
                    .HasColumnName("username");

                entity.HasOne(d => d.IdGroupNavigation)
                    .WithMany(p => p.WebUsers)
                    .HasForeignKey(d => d.IdGroup)
                    .HasConstraintName("FK_Web_User_Web_GroupUsers");
            });

            modelBuilder.Entity<_0300Bateau>(entity =>
            {
                entity.HasKey(e => e.IdBateau);

                entity.ToTable("0300_Bateau");

                entity.HasIndex(e => e.CodeBateau, "IX_0300_Bateau")
                    .IsUnique();

                entity.Property(e => e.IdBateau).HasColumnName("idBateau");

                entity.Property(e => e.CodeBateau)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("codeBateau");

                entity.Property(e => e.IdTypeBateau).HasColumnName("idTypeBateau");

                entity.Property(e => e.NomBateau)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("nomBateau");

                entity.HasOne(d => d.IdTypeBateauNavigation)
                    .WithMany(p => p._0300Bateaus)
                    .HasForeignKey(d => d.IdTypeBateau)
                    .HasConstraintName("FK_0300_Bateau_0300_TypeBateau");
            });

            modelBuilder.Entity<_0300Origine>(entity =>
            {
                entity.HasKey(e => e.IdOrigine);

                entity.ToTable("0300_Origine");

                entity.HasIndex(e => e.CodeOrigine, "IX_0300_Origine")
                    .IsUnique();

                entity.Property(e => e.IdOrigine).HasColumnName("idOrigine");

                entity.Property(e => e.CodeOrigine)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("codeOrigine");

                entity.Property(e => e.Designation)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("designation");
            });

            modelBuilder.Entity<_0300Provenance>(entity =>
            {
                entity.HasKey(e => e.IdProvenance);

                entity.ToTable("0300_Provenance");

                entity.HasIndex(e => e.CodeProvenance, "IX_0300_Provenance")
                    .IsUnique();

                entity.Property(e => e.IdProvenance).HasColumnName("idProvenance");

                entity.Property(e => e.CodeProvenance)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("codeProvenance");

                entity.Property(e => e.Designation)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("designation");
            });

            modelBuilder.Entity<_0300TypeBateau>(entity =>
            {
                entity.HasKey(e => e.IdType);

                entity.ToTable("0300_TypeBateau");

                entity.HasIndex(e => e.CodeType, "IX_0300_TypeBateau")
                    .IsUnique();

                entity.Property(e => e.IdType).HasColumnName("idType");

                entity.Property(e => e.CodeType)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("codeType");

                entity.Property(e => e.Designation)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("designation");
            });

            modelBuilder.Entity<_0301BonReceptionMp>(entity =>
            {
                entity.HasKey(e => e.IdBr);

                entity.ToTable("0301_BonReception_MP");

                entity.HasIndex(e => e.CodeBr, "IX_0301_BonReception_MP")
                    .IsUnique();

                entity.Property(e => e.IdBr).HasColumnName("idBR");

                entity.Property(e => e.Brute).HasColumnName("brute");

                entity.Property(e => e.CodeBr)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("codeBR");

                entity.Property(e => e.CoutAchat).HasColumnName("coutAchat");

                entity.Property(e => e.DateBr)
                    .HasColumnType("date")
                    .HasColumnName("dateBR");

                entity.Property(e => e.Deleted)
                    .HasColumnName("deleted")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.IdBateau).HasColumnName("idBateau");

                entity.Property(e => e.IdCamion).HasColumnName("idCamion");

                entity.Property(e => e.IdFournisseur).HasColumnName("idFournisseur");

                entity.Property(e => e.IdLot).HasColumnName("idLot");

                entity.Property(e => e.IdOrigine).HasColumnName("idOrigine");

                entity.Property(e => e.IdProvenance).HasColumnName("idProvenance");

                entity.Property(e => e.IdTransporteur).HasColumnName("idTransporteur");

                entity.Property(e => e.IsNegos).HasColumnName("isNegos");

                entity.Property(e => e.Montant).HasColumnName("montant");

                entity.Property(e => e.NbCaisse).HasColumnName("nbCaisse");

                entity.Property(e => e.NbPalette).HasColumnName("nbPalette");

                entity.Property(e => e.NbonPese)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("NBonPese");

                entity.Property(e => e.Prevalidee).HasColumnName("prevalidee");

                entity.Property(e => e.PrixAchat).HasColumnName("prixAchat");

                entity.Property(e => e.Tare).HasColumnName("tare");

                entity.Property(e => e.Validee).HasColumnName("validee");

                entity.HasOne(d => d.IdBateauNavigation)
                    .WithMany(p => p._0301BonReceptionMps)
                    .HasForeignKey(d => d.IdBateau)
                    .HasConstraintName("FK_0301_BonReception_MP_0300_Bateau");

                entity.HasOne(d => d.IdCamionNavigation)
                    .WithMany(p => p._0301BonReceptionMps)
                    .HasForeignKey(d => d.IdCamion)
                    .HasConstraintName("FK_0301_BonReception_MP_0700_Camion");

                entity.HasOne(d => d.IdFournisseurNavigation)
                    .WithMany(p => p._0301BonReceptionMps)
                    .HasForeignKey(d => d.IdFournisseur)
                    .HasConstraintName("FK_0301_BonReception_MP_0400_Fournisseur");

                entity.HasOne(d => d.IdLotNavigation)
                    .WithMany(p => p._0301BonReceptionMps)
                    .HasForeignKey(d => d.IdLot)
                    .HasConstraintName("FK_0301_BonReception_MP_0301_Lot_MP");

                entity.HasOne(d => d.IdOrigineNavigation)
                    .WithMany(p => p._0301BonReceptionMps)
                    .HasForeignKey(d => d.IdOrigine)
                    .HasConstraintName("FK_0301_BonReception_MP_0300_Origine");

                entity.HasOne(d => d.IdProvenanceNavigation)
                    .WithMany(p => p._0301BonReceptionMps)
                    .HasForeignKey(d => d.IdProvenance)
                    .HasConstraintName("FK_0301_BonReception_MP_0300_Provenance");

                entity.HasOne(d => d.IdTransporteurNavigation)
                    .WithMany(p => p._0301BonReceptionMps)
                    .HasForeignKey(d => d.IdTransporteur)
                    .HasConstraintName("FK_0301_BonReception_MP_0700_Transporteur");
            });

            modelBuilder.Entity<_0301DetailsReceptionMp>(entity =>
            {
                entity.HasKey(e => e.IdDetailsBr);

                entity.ToTable("0301_DetailsReceptionMP");

                entity.HasIndex(e => e.CodeDetailsBr, "IX_0301_DetailsReceptionMP")
                    .IsUnique();

                entity.Property(e => e.IdDetailsBr).HasColumnName("idDetailsBR");

                entity.Property(e => e.CodeDetailsBr)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("codeDetailsBR");

                entity.Property(e => e.Eventre).HasColumnName("eventre");

                entity.Property(e => e.Histamine).HasColumnName("histamine");

                entity.Property(e => e.IdArticle).HasColumnName("idArticle");

                entity.Property(e => e.IdBr).HasColumnName("idBR");

                entity.Property(e => e.IdMoule).HasColumnName("idMoule");

                entity.Property(e => e.IdQualite).HasColumnName("idQualite");

                entity.Property(e => e.MntAchat).HasColumnName("mntAchat");

                entity.Property(e => e.Pu).HasColumnName("PU");

                entity.Property(e => e.QtePayee).HasColumnName("qtePayee");

                entity.Property(e => e.QteRecue).HasColumnName("qteRecue");

                entity.HasOne(d => d.IdArticleNavigation)
                    .WithMany(p => p._0301DetailsReceptionMps)
                    .HasForeignKey(d => d.IdArticle)
                    .HasConstraintName("FK_0301_DetailsReceptionMP_0400_Article");

                entity.HasOne(d => d.IdBrNavigation)
                    .WithMany(p => p._0301DetailsReceptionMps)
                    .HasForeignKey(d => d.IdBr)
                    .HasConstraintName("FK_0301_DetailsReceptionMP_0301_BonReception_MP");

                entity.HasOne(d => d.IdMouleNavigation)
                    .WithMany(p => p._0301DetailsReceptionMps)
                    .HasForeignKey(d => d.IdMoule)
                    .HasConstraintName("FK_0301_DetailsReceptionMP_0600_Moule");

                entity.HasOne(d => d.IdQualiteNavigation)
                    .WithMany(p => p._0301DetailsReceptionMps)
                    .HasForeignKey(d => d.IdQualite)
                    .HasConstraintName("FK_0301_DetailsReceptionMP_1100_Qualite");
            });

            modelBuilder.Entity<_0301LotMp>(entity =>
            {
                entity.HasKey(e => e.IdLot);

                entity.ToTable("0301_Lot_MP");

                entity.HasIndex(e => e.CodeLot, "IX_0301_Lot_MP")
                    .IsUnique();

                entity.Property(e => e.IdLot).HasColumnName("idLot");

                entity.Property(e => e.CodeLot)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("codeLot");

                entity.Property(e => e.Deleted)
                    .HasColumnName("deleted")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Journee).HasColumnType("datetime");
            });

            modelBuilder.Entity<_0400Article>(entity =>
            {
                entity.HasKey(e => e.IdArticle);

                entity.ToTable("0400_Article");

                entity.HasIndex(e => e.CodeArticle, "IX_0400_Article")
                    .IsUnique();

                entity.Property(e => e.IdArticle).HasColumnName("idArticle");

                entity.Property(e => e.CodeArticle)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("codeArticle");

                entity.Property(e => e.Designation)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("designation");
            });

            modelBuilder.Entity<_0400Fournisseur>(entity =>
            {
                entity.HasKey(e => e.IdFournisseur);

                entity.ToTable("0400_Fournisseur");

                entity.HasIndex(e => e.CodeFournisseur, "IX_0400_Fournisseur")
                    .IsUnique();

                entity.Property(e => e.IdFournisseur).HasColumnName("idFournisseur");

                entity.Property(e => e.Adresse)
                    .HasMaxLength(200)
                    .HasColumnName("adresse");

                entity.Property(e => e.CodeFournisseur)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("codeFournisseur");

                entity.Property(e => e.CodePostal)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("codePostal");

                entity.Property(e => e.DateAnouveau)
                    .HasColumnType("date")
                    .HasColumnName("dateAnouveau");

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("email");

                entity.Property(e => e.Fax)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("fax");

                entity.Property(e => e.IsFrsCharges).HasColumnName("isFrsCharges");

                entity.Property(e => e.IsFrsMp).HasColumnName("isFrsMP");

                entity.Property(e => e.IsFrsPf).HasColumnName("isFrsPF");

                entity.Property(e => e.RaisonSociale)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("raisonSociale");

                entity.Property(e => e.Site)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("site");

                entity.Property(e => e.Tel)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("tel");

                entity.Property(e => e.Ville)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("ville");
            });

            modelBuilder.Entity<_0600Moule>(entity =>
            {
                entity.HasKey(e => e.IdMoule);

                entity.ToTable("0600_Moule");

                entity.HasIndex(e => e.CodeMoule, "IX_0600_Moule");

                entity.Property(e => e.IdMoule).HasColumnName("idMoule");

                entity.Property(e => e.CodeMoule)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("codeMoule");

                entity.Property(e => e.Designation)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("designation");
            });

            modelBuilder.Entity<_0700Camion>(entity =>
            {
                entity.HasKey(e => e.IdCamion);

                entity.ToTable("0700_Camion");

                entity.HasIndex(e => e.Matricule, "IX_0700_Camion")
                    .IsUnique();

                entity.Property(e => e.IdCamion).HasColumnName("idCamion");

                entity.Property(e => e.IdTransporteur).HasColumnName("idTransporteur");

                entity.Property(e => e.Matricule)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("matricule");

                entity.HasOne(d => d.IdTransporteurNavigation)
                    .WithMany(p => p._0700Camions)
                    .HasForeignKey(d => d.IdTransporteur)
                    .HasConstraintName("FK_0700_Camion_0700_Transporteur");
            });

            modelBuilder.Entity<_0700Transporteur>(entity =>
            {
                entity.HasKey(e => e.IdTransporteur);

                entity.ToTable("0700_Transporteur");

                entity.HasIndex(e => e.CodeTransporteur, "IX_0700_Transporteur")
                    .IsUnique();

                entity.Property(e => e.IdTransporteur).HasColumnName("idTransporteur");

                entity.Property(e => e.Adresse)
                    .HasMaxLength(200)
                    .HasColumnName("adresse");

                entity.Property(e => e.CodeTransporteur)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("codeTransporteur");

                entity.Property(e => e.RaisonSociale)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("raisonSociale");

                entity.Property(e => e.Tel)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("tel");
            });

            modelBuilder.Entity<_1100Qualite>(entity =>
            {
                entity.HasKey(e => e.IdQualite);

                entity.ToTable("1100_Qualite");

                entity.HasIndex(e => e.CodeQualite, "IX_1100_Qualite")
                    .IsUnique();

                entity.Property(e => e.IdQualite).HasColumnName("idQualite");

                entity.Property(e => e.CodeQualite)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("codeQualite");

                entity.Property(e => e.Designation)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("designation");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
