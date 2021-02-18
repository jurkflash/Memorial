using System.Data.Entity;
using Memorial.Core.Domain;
using Memorial.Persistence.EntityConfigurations;

namespace Memorial.Persistence
{
    public class MemorialContext : DbContext
    {
        public MemorialContext()
            : base("name=MemorialContext")
        {
            this.Configuration.LazyLoadingEnabled = false;
        }

        public DbSet<MaritalType> MaritalTypes { get; set; }
        public DbSet<GenderType> GenderTypes { get; set; }
        public DbSet<NationalityType> NationalityTypes { get; set; }
        public DbSet<RelationshipType> RelationshipTypes { get; set; }
        public DbSet<ReligionType> ReligionTypes { get; set; }
        public DbSet<PaymentMethod> PaymentMethods { get; set; }
        public DbSet<Site> Sites { get; set; }
        public DbSet<Applicant> Applicants { get; set; }
        public DbSet<Deceased> Deceaseds { get; set; }
        public DbSet<FengShuiMaster> FengShuiMasters { get; set; }
        public DbSet<FuneralCompany> FuneralCompanies { get; set; }
        public DbSet<PlotArea> PlotAreas { get; set; }
        public DbSet<PlotType> PlotTypes { get; set; }
        public DbSet<Plot> Plots { get; set; }
        public DbSet<PlotItem> PlotItems { get; set; }
        public DbSet<PlotNumber> PlotNumbers { get; set; }
        public DbSet<Ancestor> Ancestors { get; set; }
        public DbSet<QuadrangleArea> QuadrangleAreas { get; set; }
        public DbSet<QuadrangleType> QuadrangleTypes { get; set; }
        public DbSet<Quadrangle> Quadrangles { get; set; }
        public DbSet<PlotLandscapeCompany> PlotLandscapeCompanies { get; set; }
        public DbSet<Space> Spaces { get; set; }
        public DbSet<Miscellaneous> Miscellaneous { get; set; }
        public DbSet<QuadrangleCentre> QuadrangleCentres { get; set; }
        public DbSet<Cremation> Cremations { get; set; }
        public DbSet<CremationItem> CremationItems { get; set; }
        public DbSet<Urn> Urns { get; set; }
        public DbSet<QuadrangleItem> QuadrangleItems { get; set; }
        public DbSet<QuadrangleNumber> QuadrangleNumbers { get; set; }
        public DbSet<SpaceItem> SpaceItems { get; set; }
        public DbSet<SpaceNumber> SpaceNumbers { get; set; }
        public DbSet<CremationNumber> CremationNumbers { get; set; }
        public DbSet<MiscellaneousItem> MiscellaneousItems { get; set; }
        public DbSet<MiscellaneousNumber> MiscellaneousNumbers { get; set; }
        public DbSet<AncestorArea> AncestorAreas { get; set; }
        public DbSet<AncestorItem> AncestorItems { get; set; }
        public DbSet<AncestorNumber> AncestorNumbers { get; set; }
        public DbSet<UrnItem> UrnItems { get; set; }
        public DbSet<UrnNumber> UrnNumbers { get; set; }
        public DbSet<SpaceTransaction> SpaceTransactions { get; set; }
        public DbSet<MiscellaneousTransaction> MiscellaneousTransactions { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<Receipt> Receipts { get; set; }
        public DbSet<QuadrangleTransaction> QuadrangleTransactions { get; set; }
        public DbSet<PlotTransaction> PlotTransactions { get; set; }
        public DbSet<UrnTransaction> UrnTransactions { get; set; }
        public DbSet<CremationTransaction> CremationTransactions { get; set; }
        public DbSet<AncestorTransaction> AncestorTransactions { get; set; }
        public DbSet<Catalog> Catalogs { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new MaritalTypeConfiguration());
            modelBuilder.Configurations.Add(new GenderTypeConfiguration());
            modelBuilder.Configurations.Add(new NationalityTypeConfiguration());
            modelBuilder.Configurations.Add(new RelationshipTypeConfiguration());
            modelBuilder.Configurations.Add(new ReligionTypeConfiguration());
            modelBuilder.Configurations.Add(new PaymentMethodConfiguration());
            modelBuilder.Configurations.Add(new SiteConfiguration());
            modelBuilder.Configurations.Add(new ApplicantConfiguration());
            modelBuilder.Configurations.Add(new DeceasedConfiguration());
            modelBuilder.Configurations.Add(new FengShuiMasterConfiguration());
            modelBuilder.Configurations.Add(new FuneralCompanyConfiguration());
            modelBuilder.Configurations.Add(new PlotAreaConfiguration());
            modelBuilder.Configurations.Add(new PlotTypeConfiguration());
            modelBuilder.Configurations.Add(new PlotConfiguration());
            modelBuilder.Configurations.Add(new AncestorConfiguration());
            modelBuilder.Configurations.Add(new QuadrangleAreaConfiguration());
            modelBuilder.Configurations.Add(new QuadrangleTypeConfiguration());
            modelBuilder.Configurations.Add(new QuadrangleConfiguration());
            modelBuilder.Configurations.Add(new PlotLandscapeCompanyConfiguration());
            modelBuilder.Configurations.Add(new SpaceConfiguration());
            modelBuilder.Configurations.Add(new MiscellaneousConfiguration());
            modelBuilder.Configurations.Add(new QuadrangleCentreConfiguration());
            modelBuilder.Configurations.Add(new CremationConfiguration());
            modelBuilder.Configurations.Add(new UrnConfiguration());
            modelBuilder.Configurations.Add(new QuadrangleItemConfiguration());
            modelBuilder.Configurations.Add(new QuadrangleNumberConfiguration());
            modelBuilder.Configurations.Add(new SpaceItemConfiguration());
            modelBuilder.Configurations.Add(new SpaceNumberConfiguration());
            modelBuilder.Configurations.Add(new PlotItemConfiguration());
            modelBuilder.Configurations.Add(new PlotNumberConfiguration());
            modelBuilder.Configurations.Add(new CremationItemConfiguration());
            modelBuilder.Configurations.Add(new CremationNumberConfiguration());
            modelBuilder.Configurations.Add(new MiscellaneousItemConfiguration());
            modelBuilder.Configurations.Add(new MiscellaneousNumberConfiguration());
            modelBuilder.Configurations.Add(new AncestorAreaConfiguration());
            modelBuilder.Configurations.Add(new AncestorItemConfiguration());
            modelBuilder.Configurations.Add(new AncestorNumberConfiguration());
            modelBuilder.Configurations.Add(new UrnItemConfiguration());
            modelBuilder.Configurations.Add(new UrnNumberConfiguration());
            modelBuilder.Configurations.Add(new SpaceTransactionConfiguration());
            modelBuilder.Configurations.Add(new MiscellaneousTransactionConfiguration());
            modelBuilder.Configurations.Add(new InvoiceConfiguration());
            modelBuilder.Configurations.Add(new ReceiptConfiguration());
            modelBuilder.Configurations.Add(new QuadrangleTransactionConfiguration());
            modelBuilder.Configurations.Add(new PlotTransactionConfiguration());
            modelBuilder.Configurations.Add(new UrnTransactionConfiguration());
            modelBuilder.Configurations.Add(new CremationTransactionConfiguration());
            modelBuilder.Configurations.Add(new AncestorTransactionConfiguration());
            modelBuilder.Configurations.Add(new CatalogConfiguration());
        }
    }
}