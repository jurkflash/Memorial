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
        IFengShuiMasterRepository FengShuiMasters { get; }
        IFuneralCompanyRepository FuneralCompanies { get; }

        IPlotRepository Plots { get; }
        IPlotAreaRepository PlotAreas { get; }
        IPlotTypeRepository PlotTypes { get; }
        IPlotItemRepository PlotItems { get; }
        IPlotNumberRepository PlotNumbers { get; }
        IPlotTransactionRepository PlotTransactions { get; }

        IAncestorRepository Ancestors { get; }
        IAncestorAreaRepository AncestorAreas { get; }
        IAncestorItemRepository AncestorItems { get; }
        IAncestorNumberRepository AncestorNumbers { get; }
        IAncestorTransactionRepository AncestorTransactions { get; }

        IPlotLandscapeCompanyRepository PlotLandscapeCompanies { get; }

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

        IQuadrangleRepository Quadrangles { get; }
        IQuadrangleAreaRepository QuadrangleAreas { get; }
        IQuadrangleTypeRepository QuadrangleTypes { get; }
        IQuadrangleItemRepository QuadrangleItems { get; }
        IQuadrangleCentreRepository QuadrangleCentres { get; }
        IQuadrangleNumberRepository QuadrangleNumbers { get; }
        IQuadrangleTransactionRepository QuadrangleTransactions { get; }

        ISpaceRepository Spaces { get; }
        ISpaceItemRepository SpaceItems { get; }
        ISpaceNumberRepository SpaceNumbers { get; }
        ISpaceTransactionRepository SpaceTransactions { get; }

        IInvoiceRepository Invoices { get; }

        IReceiptRepository Receipts { get; }

        int Complete();
    }
}