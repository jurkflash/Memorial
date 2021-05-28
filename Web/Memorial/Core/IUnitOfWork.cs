using System;
using Memorial.Core.Repositories;

namespace Memorial.Core
{
    public interface IUnitOfWork : IDisposable
    {
        ICatalogRepository Catalogs { get; }

        IGenderTypeRepository GenderTypes { get; }
        IMaritalTypeRepository MaritalTypes { get; }
        INationalityTypeRepository NationalityTypes { get; }
        IRelationshipTypeRepository RelationshipTypes { get; }
        IReligionTypeRepository ReligionTypes { get; }
        IPaymentMethodRepository PaymentMethods { get; }
        ISiteRepository Sites { get; }
        IApplicantRepository Applicants { get; }
        IDeceasedRepository Deceaseds { get; }
        IApplicantDeceasedRepository ApplicantDeceaseds { get; }
        IFengShuiMasterRepository FengShuiMasters { get; }
        IFuneralCompanyRepository FuneralCompanies { get; }

        IPlotRepository Plots { get; }
        ICemeteryAreaRepository CemeteryAreas { get; }
        IPlotTypeRepository PlotTypes { get; }
        ICemeteryItemRepository CemeteryItems { get; }
        IPlotNumberRepository PlotNumbers { get; }
        ICemeteryTransactionRepository CemeteryTransactions { get; }
        IPlotTrackingRepository PlotTrackings { get; }

        IAncestralTabletRepository AncestralTablets { get; }
        IAncestralTabletTrackingRepository AncestralTabletTrackings { get; }
        IAncestralTabletAreaRepository AncestralTabletAreas { get; }
        IAncestralTabletItemRepository AncestralTabletItems { get; }
        IAncestralTabletNumberRepository AncestralTabletNumbers { get; }
        IAncestralTabletTransactionRepository AncestralTabletTransactions { get; }

        ICemeteryLandscapeCompanyRepository CemeteryLandscapeCompanies { get; }

        IUrnRepository Urns { get; }
        IUrnItemRepository UrnItems { get; }
        IUrnNumberRepository UrnNumbers { get; }
        IUrnTransactionRepository UrnTransactions { get; }

        IMiscellaneousRepository Miscellaneous { get; }
        IMiscellaneousItemRepository MiscellaneousItems { get; }
        IMiscellaneousNumberRepository MiscellaneousNumbers { get; }
        IMiscellaneousTransactionRepository MiscellaneousTransactions { get; }

        ICremationRepository Cremations { get; }
        ICremationItemRepository CremationItems { get; }
        ICremationNumberRepository CremationNumbers { get; }
        ICremationTransactionRepository CremationTransactions { get; }

        INicheRepository Niches { get; }
        IColumbariumTrackingRepository ColumbariumTrackings { get; }
        IColumbariumAreaRepository ColumbariumAreas { get; }
        INicheTypeRepository NicheTypes { get; }
        IColumbariumItemRepository ColumbariumItems { get; }
        IColumbariumCentreRepository ColumbariumCentres { get; }
        IColumbariumNumberRepository ColumbariumNumbers { get; }
        IColumbariumTransactionRepository ColumbariumTransactions { get; }

        ISpaceRepository Spaces { get; }
        ISpaceItemRepository SpaceItems { get; }
        ISpaceNumberRepository SpaceNumbers { get; }
        ISpaceTransactionRepository SpaceTransactions { get; }

        IInvoiceRepository Invoices { get; }

        IReceiptRepository Receipts { get; }

        int Complete();
    }
}