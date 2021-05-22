using Memorial.Core;
using Memorial.Core.Repositories;
using Memorial.Persistence.Repositories;

namespace Memorial.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly MemorialContext _context;

        public UnitOfWork(MemorialContext context)
        {
            _context = context;
            GenderTypes = new GenderTypeRepository(_context);
            MaritalTypes = new MaritalTypeRepository(_context);
            NationalityTypes = new NationalityTypeRepository(_context);
            RelationshipTypes = new RelationshipTypeRepository(_context);
            ReligionTypes = new ReligionTypeRepository(_context);
            PaymentMethods = new PaymentMethodRepository(_context);
            Sites = new SiteRepository(_context);
            Applicants = new ApplicantRepository(_context);
            Deceaseds = new DeceasedRepository(_context);
            ApplicantDeceaseds = new ApplicantDeceasedRepository(_context);
            FengShuiMasters = new FengShuiMasterRepository(_context);
            FuneralCompanies = new FuneralCompanyRepository(_context);
            PlotLandscapeCompanies = new PlotLandscapeCompanyRepository(_context);

            Cremations = new CremationRepository(_context);
            CremationItems = new CremationItemRepository(_context);
            CremationNumbers = new CremationNumberRepository(_context);
            CremationTransactions = new CremationTransactionRepository(_context);

            Miscellaneous = new MiscellaneousRepository(_context);
            MiscellaneousItems = new MiscellaneousItemRepository(_context);
            MiscellaneousNumbers = new MiscellaneousNumberRepository(_context);
            MiscellaneousTransactions = new MiscellaneousTransactionRepository(_context);

            Urns = new UrnRepository(_context);
            UrnItems = new UrnItemRepository(_context);
            UrnNumbers = new UrnNumberRepository(_context);
            UrnTransactions = new UrnTransactionRepository(_context);

            ColumbariumAreas = new ColumbariumAreaRepository(_context);
            NicheTypes = new NicheTypeRepository(_context);
            Niches = new NicheRepository(_context);
            ColumbariumTrackings = new ColumbariumTrackingRepository(_context);
            ColumbariumCentres = new ColumbariumCentreRepository(_context);
            ColumbariumItems = new ColumbariumItemRepository(_context);
            ColumbariumNumbers = new ColumbariumNumberRepository(_context);
            ColumbariumTransactions = new ColumbariumTransactionRepository(_context);

            Spaces = new SpaceRepository(_context);
            SpaceItems = new SpaceItemRepository(_context);
            SpaceNumbers = new SpaceNumberRepository(_context);
            SpaceTransactions = new SpaceTransactionRepository(_context);

            CemeteryAreas = new CemeteryAreaRepository(_context);
            PlotTypes = new PlotTypeRepository(_context);
            Plots = new PlotRepository(_context);
            CemeteryItems = new CemeteryItemRepository(_context);
            PlotNumbers = new PlotNumberRepository(_context);
            CemeteryTransactions = new CemeteryTransactionRepository(_context);
            PlotTrackings = new PlotTrackingRepository(_context);

            Ancestors = new AncestorRepository(_context);
            AncestralTabletAreas = new AncestralTabletAreaRepository(_context);
            AncestralTabletItems = new AncestralTabletItemRepository(_context);
            AncestralTabletNumbers = new AncestralTabletNumberRepository(_context);
            AncestralTabletTransactions = new AncestralTabletTransactionRepository(_context);
            AncestorTrackings = new AncestorTrackingRepository(_context);

            Invoices = new InvoiceRepository(_context);
            Receipts = new ReceiptRepository(_context);
            
            Catalogs = new CatalogRepository(_context);
        }

        public IGenderTypeRepository GenderTypes { get; private set; }
        public IMaritalTypeRepository MaritalTypes { get; private set; }
        public INationalityTypeRepository NationalityTypes { get; private set; }
        public IRelationshipTypeRepository RelationshipTypes { get; private set; }
        public IReligionTypeRepository ReligionTypes { get; private set; }
        public IPaymentMethodRepository PaymentMethods { get; private set; }
        public ISiteRepository Sites { get; private set; }
        public IApplicantRepository Applicants { get; private set; }
        public IDeceasedRepository Deceaseds { get; private set; }
        public IApplicantDeceasedRepository ApplicantDeceaseds { get; private set; }
        public IFengShuiMasterRepository FengShuiMasters { get; private set; }
        public IFuneralCompanyRepository FuneralCompanies { get; private set; }
        
        public IColumbariumCentreRepository ColumbariumCentres { get; private set; }
        public IUrnRepository Urns { get; private set; }
        public IColumbariumItemRepository ColumbariumItems { get; private set; }
        public IColumbariumNumberRepository ColumbariumNumbers { get; private set; }
        public IColumbariumAreaRepository ColumbariumAreas { get; private set; }
        public INicheTypeRepository NicheTypes { get; private set; }
        public INicheRepository Niches { get; private set; }
        public IColumbariumTrackingRepository ColumbariumTrackings { get; private set; }
        public IColumbariumTransactionRepository ColumbariumTransactions { get; private set; }

        public ICemeteryAreaRepository CemeteryAreas { get; private set; }
        public IPlotTypeRepository PlotTypes { get; private set; }
        public IPlotRepository Plots { get; private set; }
        public ICemeteryItemRepository CemeteryItems { get; private set; }
        public IPlotNumberRepository PlotNumbers { get; private set; }
        public ICemeteryTransactionRepository CemeteryTransactions { get; private set; }
        public IPlotTrackingRepository PlotTrackings { get; private set; }

        public IPlotLandscapeCompanyRepository PlotLandscapeCompanies { get; private set; }

        public ICremationRepository Cremations { get; private set; }
        public ICremationItemRepository CremationItems { get; private set; }
        public ICremationNumberRepository CremationNumbers { get; private set; }
        public ICremationTransactionRepository CremationTransactions { get; private set; }

        public IMiscellaneousRepository Miscellaneous { get; private set; }
        public IMiscellaneousItemRepository MiscellaneousItems { get; private set; }
        public IMiscellaneousNumberRepository MiscellaneousNumbers { get; private set; }
        public IMiscellaneousTransactionRepository MiscellaneousTransactions { get; private set; }

        public IAncestorRepository Ancestors { get; private set; }
        public IAncestralTabletAreaRepository AncestralTabletAreas { get; private set; }
        public IAncestralTabletItemRepository AncestralTabletItems { get; private set; }
        public IAncestralTabletNumberRepository AncestralTabletNumbers { get; private set; }
        public IAncestralTabletTransactionRepository AncestralTabletTransactions { get; private set; }
        public IAncestorTrackingRepository AncestorTrackings { get; private set; }

        public IUrnItemRepository UrnItems { get; private set; }
        public IUrnNumberRepository UrnNumbers { get; private set; }
        public IUrnTransactionRepository UrnTransactions { get; private set; }

        public ISpaceRepository Spaces { get; private set; }
        public ISpaceItemRepository SpaceItems { get; private set; }
        public ISpaceNumberRepository SpaceNumbers { get; private set; }
        public ISpaceTransactionRepository SpaceTransactions { get; private set; }
        
        public IInvoiceRepository Invoices { get; private set; }
        public IReceiptRepository Receipts { get; private set; }
        
        public ICatalogRepository Catalogs { get; private set; }

        public int Complete()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}