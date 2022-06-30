using System;
using System.Data.Entity;
using System.Linq;
using Memorial.Core.Domain;
using Memorial.Persistence.EntityConfigurations;
using Z.EntityFramework.Plus;

namespace Memorial.Persistence
{
    public class MemorialContext : DbContext
    {
        public MemorialContext()
            : base("name=MemorialContext")
        {
            this.Configuration.LazyLoadingEnabled = false;

            QueryFilterManager.Filter<Base>(x => x.Where(a => a.ActiveStatus == true));

            QueryFilterManager.InitilizeGlobalFilter(this);
        }

        public DbSet<AccessControl> AccessControls { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<SubProductService> SubProductServices { get; set; }
        public DbSet<MaritalType> MaritalTypes { get; set; }
        public DbSet<GenderType> GenderTypes { get; set; }
        public DbSet<NationalityType> NationalityTypes { get; set; }
        public DbSet<RelationshipType> RelationshipTypes { get; set; }
        public DbSet<ReligionType> ReligionTypes { get; set; }
        public DbSet<PaymentMethod> PaymentMethods { get; set; }
        public DbSet<Site> Sites { get; set; }
        public DbSet<Applicant> Applicants { get; set; }
        public DbSet<Deceased> Deceaseds { get; set; }
        public DbSet<ApplicantDeceased> ApplicantDeceaseds { get; set; }
        public DbSet<FengShuiMaster> FengShuiMasters { get; set; }
        public DbSet<FuneralCompany> FuneralCompanies { get; set; }

        public DbSet<CemeteryArea> CemeteryAreas { get; set; }
        public DbSet<PlotType> PlotTypes { get; set; }
        public DbSet<Plot> Plots { get; set; }
        public DbSet<CemeteryItem> CemeteryItems { get; set; }
        public DbSet<CemeteryNumber> CemeteryNumbers { get; set; }
        public DbSet<CemeteryLandscapeCompany> CemeteryLandscapeCompanies { get; set; }
        public DbSet<CemeteryTransaction> CemeteryTransactions { get; set; }
        public DbSet<CemeteryTracking> CemeteryTrackings { get; set; }

        public DbSet<ColumbariumArea> ColumbariumAreas { get; set; }
        public DbSet<NicheType> NicheTypes { get; set; }
        public DbSet<Niche> Niches { get; set; }
        public DbSet<ColumbariumTracking> ColumbariumTrackings { get; set; }
        public DbSet<ColumbariumCentre> ColumbariumCentres { get; set; }
        public DbSet<ColumbariumItem> ColumbariumItems { get; set; }
        public DbSet<ColumbariumNumber> ColumbariumNumbers { get; set; }
        public DbSet<ColumbariumTransaction> ColumbariumTransactions { get; set; }

        public DbSet<Space> Spaces { get; set; }
        public DbSet<SpaceItem> SpaceItems { get; set; }
        public DbSet<SpaceNumber> SpaceNumbers { get; set; }
        public DbSet<SpaceTransaction> SpaceTransactions { get; set; }

        public DbSet<Miscellaneous> Miscellaneous { get; set; }
        public DbSet<MiscellaneousItem> MiscellaneousItems { get; set; }
        public DbSet<MiscellaneousNumber> MiscellaneousNumbers { get; set; }
        public DbSet<MiscellaneousTransaction> MiscellaneousTransactions { get; set; }

        public DbSet<Cremation> Cremations { get; set; }
        public DbSet<CremationItem> CremationItems { get; set; }
        public DbSet<CremationNumber> CremationNumbers { get; set; }
        public DbSet<CremationTransaction> CremationTransactions { get; set; }

        public DbSet<Urn> Urns { get; set; }
        public DbSet<UrnItem> UrnItems { get; set; }
        public DbSet<UrnNumber> UrnNumbers { get; set; }
        public DbSet<UrnTransaction> UrnTransactions { get; set; }

        public DbSet<AncestralTablet> AncestralTablets { get; set; }
        public DbSet<AncestralTabletTransaction> AncestralTabletTransactions { get; set; }
        public DbSet<AncestralTabletArea> AncestralTabletAreas { get; set; }
        public DbSet<AncestralTabletItem> AncestralTabletItems { get; set; }
        public DbSet<AncestralTabletNumber> AncestralTabletNumbers { get; set; }
        public DbSet<AncestralTabletTracking> AncestralTabletTrackings { get; set; }
        
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<Receipt> Receipts { get; set; }
        
        
        public DbSet<Catalog> Catalogs { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new AccessControlConfiguration());
            modelBuilder.Configurations.Add(new ProductConfiguration());
            modelBuilder.Configurations.Add(new SubProductServiceConfiguration());

            modelBuilder.Configurations.Add(new MaritalTypeConfiguration());
            modelBuilder.Configurations.Add(new GenderTypeConfiguration());
            modelBuilder.Configurations.Add(new NationalityTypeConfiguration());
            modelBuilder.Configurations.Add(new RelationshipTypeConfiguration());
            modelBuilder.Configurations.Add(new ReligionTypeConfiguration());
            modelBuilder.Configurations.Add(new PaymentMethodConfiguration());
            modelBuilder.Configurations.Add(new SiteConfiguration());
            modelBuilder.Configurations.Add(new ApplicantConfiguration());
            modelBuilder.Configurations.Add(new DeceasedConfiguration());
            modelBuilder.Configurations.Add(new ApplicantDeceasedConfiguration());
            modelBuilder.Configurations.Add(new FengShuiMasterConfiguration());
            modelBuilder.Configurations.Add(new FuneralCompanyConfiguration());

            modelBuilder.Configurations.Add(new CemeteryTransactionConfiguration());
            modelBuilder.Configurations.Add(new CemeteryAreaConfiguration());
            modelBuilder.Configurations.Add(new PlotTypeConfiguration());
            modelBuilder.Configurations.Add(new PlotConfiguration());
            modelBuilder.Configurations.Add(new CemeteryLandscapeCompanyConfiguration());
            modelBuilder.Configurations.Add(new CemeteryItemConfiguration());
            modelBuilder.Configurations.Add(new CemeteryNumberConfiguration());
            modelBuilder.Configurations.Add(new CemeteryTrackingConfiguration());

            modelBuilder.Configurations.Add(new AncestralTabletConfiguration());
            modelBuilder.Configurations.Add(new AncestralTabletTrackingConfiguration());
            modelBuilder.Configurations.Add(new AncestralTabletAreaConfiguration());
            modelBuilder.Configurations.Add(new AncestralTabletItemConfiguration());
            modelBuilder.Configurations.Add(new AncestralTabletNumberConfiguration());
            modelBuilder.Configurations.Add(new AncestralTabletTransactionConfiguration());

            modelBuilder.Configurations.Add(new ColumbariumAreaConfiguration());
            modelBuilder.Configurations.Add(new NicheTypeConfiguration());
            modelBuilder.Configurations.Add(new NicheConfiguration());
            modelBuilder.Configurations.Add(new ColumbariumCentreConfiguration());
            modelBuilder.Configurations.Add(new ColumbariumItemConfiguration());
            modelBuilder.Configurations.Add(new ColumbariumNumberConfiguration());
            modelBuilder.Configurations.Add(new ColumbariumTransactionConfiguration());
            modelBuilder.Configurations.Add(new ColumbariumTrackingConfiguration());
           
            modelBuilder.Configurations.Add(new CremationConfiguration());
            modelBuilder.Configurations.Add(new CremationItemConfiguration());
            modelBuilder.Configurations.Add(new CremationNumberConfiguration());
            modelBuilder.Configurations.Add(new CremationTransactionConfiguration());

            modelBuilder.Configurations.Add(new UrnConfiguration());
            modelBuilder.Configurations.Add(new UrnItemConfiguration());
            modelBuilder.Configurations.Add(new UrnNumberConfiguration());
            modelBuilder.Configurations.Add(new UrnTransactionConfiguration());

            modelBuilder.Configurations.Add(new SpaceConfiguration());
            modelBuilder.Configurations.Add(new SpaceItemConfiguration());
            modelBuilder.Configurations.Add(new SpaceNumberConfiguration());
            modelBuilder.Configurations.Add(new SpaceTransactionConfiguration());

            modelBuilder.Configurations.Add(new MiscellaneousConfiguration());
            modelBuilder.Configurations.Add(new MiscellaneousItemConfiguration());
            modelBuilder.Configurations.Add(new MiscellaneousNumberConfiguration());
            modelBuilder.Configurations.Add(new MiscellaneousTransactionConfiguration());
            
            modelBuilder.Configurations.Add(new InvoiceConfiguration());
            modelBuilder.Configurations.Add(new ReceiptConfiguration());
           
            modelBuilder.Configurations.Add(new CatalogConfiguration());
        }

        public override int SaveChanges()
        {
            UpdateAuditColumns();

            return base.SaveChanges();
        }

        private void UpdateAuditColumns()
        {
            var modifiedEntries = ChangeTracker.Entries()
                                    .Where(a => a.Entity is Base && (a.State == EntityState.Added || a.State == EntityState.Modified || a.State == EntityState.Deleted));

            //get user id from http context if available (passed from UserInformationMiddleware)
            int? userId = 1;

            //if (_httpContextAccessor?.HttpContext?.Items["UserId"] != null)
            //{
            //    userId = (long)_httpContextAccessor?.HttpContext.Items["UserId"];
            //}

            foreach (var entry in modifiedEntries)
            {
                if (entry.Entity is Base entity)
                {
                    DateTime now = DateTime.Now;

                    if (entry.State == EntityState.Added)
                    {
                        if (entity.ActiveStatus == default)
                        {
                            entity.ActiveStatus = true;
                        }

                        if (entity.CreatedById == default && userId.HasValue)
                        {
                            entity.CreatedById = userId.Value;
                        }

                        entity.CreatedDate = now;
                    }
                    else if (entry.State == EntityState.Deleted)
                    {
                        entry.State = EntityState.Modified;
                        entity.ActiveStatus = false;
                        entity.DeletedById = userId.Value;
                        entity.DeletedDate = now;
                    }
                    else
                    {
                        entity.ModifiedById = userId.Value;
                        entity.ModifiedDate = now;
                    }
                }
            }
        }

    }
}