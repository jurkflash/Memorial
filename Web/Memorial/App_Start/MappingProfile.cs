using AutoMapper;
using Memorial.Core.Dtos;
using Memorial.Core.Domain;

namespace Memorial.App_Start
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<SpaceTransaction, SpaceBooked>()
                .ForMember(c => c.AF, opt => opt.MapFrom(x => x.AF))
                .ForMember(c => c.FromDate, opt => opt.MapFrom(x => x.FromDate))
                .ForMember(c => c.ToDate, opt => opt.MapFrom(x => x.ToDate))
                .ForMember(c => c.SpaceName, opt => opt.MapFrom(x => x.SpaceItem.Space.Name))
                .ForMember(c => c.SpaceColorCode, opt => opt.MapFrom(x => x.SpaceItem.Space.ColorCode))
                .ForMember(c => c.TransactionRemark, opt => opt.MapFrom(x => x.Remark));



            // Domain to Dto
            CreateMap<Product, ProductDto>();

            CreateMap<SubProductService, SubProductServiceDto>()
                .ForMember(c => c.ProductDto, opt => opt.MapFrom(x => x.Product))
                .ForMember(c => c.ProductDtoId, opt => opt.MapFrom(x => x.ProductId));

            CreateMap<Catalog, CatalogDto>()
                .ForMember(c => c.ProductDto, opt => opt.MapFrom(x => x.Product))
                .ForMember(c => c.ProductDtoId, opt => opt.MapFrom(x => x.ProductId))
                .ForMember(c => c.SiteDto, opt => opt.MapFrom(x => x.Site))
                .ForMember(c => c.SiteDtoId, opt => opt.MapFrom(x => x.SiteId));

            CreateMap<Site, SiteDto>();

            CreateMap<GenderType, GenderTypeDto>();
            CreateMap<RelationshipType, RelationshipTypeDto>();
            CreateMap<MaritalType, MaritalTypeDto>();
            CreateMap<NationalityType, NationalityTypeDto>();
            CreateMap<ReligionType, ReligionTypeDto>();

            CreateMap<FengShuiMaster, FengShuiMasterDto>();
            CreateMap<FuneralCompany, FuneralCompanyDto>();
            CreateMap<CemeteryLandscapeCompany, CemeteryLandscapeCompanyDto>();


            CreateMap<Applicant, ApplicantDto>()
                .ForMember(c => c.SiteDtoId, opt => opt.MapFrom(x => x.SiteId));

            CreateMap<Deceased, DeceasedDto>()
                .ForMember(c => c.GenderTypeDto, opt => opt.MapFrom(x => x.GenderType))
                .ForMember(c => c.GenderTypeDtoId, opt => opt.MapFrom(x => x.GenderTypeId))
                .ForMember(c => c.NationalityTypeDto, opt => opt.MapFrom(x => x.NationalityType))
                .ForMember(c => c.NationalityTypeDtoId, opt => opt.MapFrom(x => x.NationalityTypeId))
                .ForMember(c => c.MaritalTypeDto, opt => opt.MapFrom(x => x.MaritalType))
                .ForMember(c => c.MaritalTypeDtoId, opt => opt.MapFrom(x => x.MaritalTypeId))
                .ForMember(c => c.ReligionTypeDto, opt => opt.MapFrom(x => x.ReligionType))
                .ForMember(c => c.ReligionTypeDtoId, opt => opt.MapFrom(x => x.ReligionTypeId));
            CreateMap<Deceased, DeceasedBriefDto>();
            CreateMap<ApplicantDeceased, ApplicantDeceasedDto>()
                .ForMember(c => c.ApplicantDto, opt => opt.MapFrom(x => x.Applicant))
                .ForMember(c => c.ApplicantDtoId, opt => opt.MapFrom(x => x.ApplicantId))
                .ForMember(c => c.DeceasedDto, opt => opt.MapFrom(x => x.Deceased))
                .ForMember(c => c.DeceasedDtoId, opt => opt.MapFrom(x => x.DeceasedId))
                .ForMember(c => c.RelationshipTypeDto, opt => opt.MapFrom(x => x.RelationshipType))
                .ForMember(c => c.RelationshipTypeDtoId, opt => opt.MapFrom(x => x.RelationshipTypeId));
            

            CreateMap<CremationTransaction, CremationTransactionDto>();

            CreateMap<Miscellaneous, MiscellaneousDto>()
                .ForMember(c => c.SiteDto, opt => opt.MapFrom(x => x.Site))
                .ForMember(c => c.SiteDtoId, opt => opt.MapFrom(x => x.SiteId));
            CreateMap<MiscellaneousItem, MiscellaneousItemDto>()
                .ForMember(c => c.SubProductServiceDto, opt => opt.MapFrom(x => x.SubProductService))
                .ForMember(c => c.SubProductServiceDtoId, opt => opt.MapFrom(x => x.SubProductServiceId))
                .ForMember(c => c.MiscellaneousDto, opt => opt.MapFrom(x => x.Miscellaneous))
                .ForMember(c => c.MiscellaneousDtoId, opt => opt.MapFrom(x => x.MiscellaneousId));
            CreateMap<MiscellaneousTransaction, MiscellaneousTransactionDto>()
                .ForMember(c => c.MiscellaneousItemDto, opt => opt.MapFrom(x => x.MiscellaneousItem))
                .ForMember(c => c.MiscellaneousItemDtoId, opt => opt.MapFrom(x => x.MiscellaneousItemId))
                .ForMember(c => c.ApplicantDto, opt => opt.MapFrom(x => x.Applicant))
                .ForMember(c => c.ApplicantDtoId, opt => opt.MapFrom(x => x.ApplicantId))
                .ForMember(c => c.CemeteryLandscapeCompanyDto, opt => opt.MapFrom(x => x.CemeteryLandscapeCompany))
                .ForMember(c => c.CemeteryLandscapeCompanyDtoId, opt => opt.MapFrom(x => x.CemeteryLandscapeCompanyId));

            CreateMap<Space, SpaceDto>()
                .ForMember(c => c.SiteDto, opt => opt.MapFrom(x => x.Site))
                .ForMember(c => c.SiteDtoId, opt => opt.MapFrom(x => x.SiteId));
            CreateMap<SpaceItem, SpaceItemDto>()
                .ForMember(c => c.SubProductServiceDto, opt => opt.MapFrom(x => x.SubProductService))
                .ForMember(c => c.SubProductServiceDtoId, opt => opt.MapFrom(x => x.SubProductServiceId))
                .ForMember(c => c.SpaceDto, opt => opt.MapFrom(x => x.Space))
                .ForMember(c => c.SpaceDtoId, opt => opt.MapFrom(x => x.SpaceId));
            CreateMap<SpaceTransaction, SpaceTransactionDto>()
                .ForMember(c => c.SpaceItemDto, opt => opt.MapFrom(x => x.SpaceItem))
                .ForMember(c => c.SpaceItemDtoId, opt => opt.MapFrom(x => x.SpaceItemId))
                .ForMember(c => c.FuneralCompanyDto, opt => opt.MapFrom(x => x.FuneralCompany))
                .ForMember(c => c.FuneralCompanyDtoId, opt => opt.MapFrom(x => x.FuneralCompanyId))
                .ForMember(c => c.ApplicantDto, opt => opt.MapFrom(x => x.Applicant))
                .ForMember(c => c.ApplicantDtoId, opt => opt.MapFrom(x => x.ApplicantId))
                .ForMember(c => c.DeceasedDto, opt => opt.MapFrom(x => x.Deceased))
                .ForMember(c => c.DeceasedDtoId, opt => opt.MapFrom(x => x.DeceasedId));

            CreateMap<Urn, UrnDto>()
                .ForMember(c => c.SiteDto, opt => opt.MapFrom(x => x.Site))
                .ForMember(c => c.SiteDtoId, opt => opt.MapFrom(x => x.SiteId));
            CreateMap<UrnItem, UrnItemDto>()
                .ForMember(c => c.SubProductServiceDto, opt => opt.MapFrom(x => x.SubProductService))
                .ForMember(c => c.SubProductServiceDtoId, opt => opt.MapFrom(x => x.SubProductServiceId))
                .ForMember(c => c.UrnDto, opt => opt.MapFrom(x => x.Urn))
                .ForMember(c => c.UrnDtoId, opt => opt.MapFrom(x => x.UrnId));
            CreateMap<UrnTransaction, UrnTransactionDto>()
                .ForMember(c => c.UrnItemDto, opt => opt.MapFrom(x => x.UrnItem))
                .ForMember(c => c.UrnItemDtoId, opt => opt.MapFrom(x => x.UrnItemId))
                .ForMember(c => c.ApplicantDto, opt => opt.MapFrom(x => x.Applicant))
                .ForMember(c => c.ApplicantDtoId, opt => opt.MapFrom(x => x.ApplicantId));

            CreateMap<Plot, PlotDto>()
                .ForMember(c => c.ApplicantDto, opt => opt.MapFrom(x => x.Applicant))
                .ForMember(c => c.ApplicantDtoId, opt => opt.MapFrom(x => x.ApplicantId))
                .ForMember(c => c.PlotTypeDto, opt => opt.MapFrom(x => x.PlotType))
                .ForMember(c => c.PlotTypeDtoId, opt => opt.MapFrom(x => x.PlotTypeId))
                .ForMember(c => c.CemeteryAreaDto, opt => opt.MapFrom(x => x.CemeteryArea))
                .ForMember(c => c.CemeteryAreaDtoId, opt => opt.MapFrom(x => x.CemeteryAreaId));
            CreateMap<CemeteryArea, CemeteryAreaDto>()
                .ForMember(c => c.SiteDto, opt => opt.MapFrom(x => x.Site))
                .ForMember(c => c.SiteDtoId, opt => opt.MapFrom(x => x.SiteId));
            CreateMap<CemeteryItem, CemeteryItemDto>()
                .ForMember(c => c.SubProductServiceDto, opt => opt.MapFrom(x => x.SubProductService))
                .ForMember(c => c.SubProductServiceDtoId, opt => opt.MapFrom(x => x.SubProductServiceId))
                .ForMember(c => c.PlotDto, opt => opt.MapFrom(x => x.Plot))
                .ForMember(c => c.PlotDtoId, opt => opt.MapFrom(x => x.PlotId));

            CreateMap<CemeteryTransaction, CemeteryTransactionDto>()
                .ForMember(c => c.CemeteryItemDto, opt => opt.MapFrom(x => x.CemeteryItem))
                .ForMember(c => c.CemeteryItemDtoId, opt => opt.MapFrom(x => x.CemeteryItemId))
                .ForMember(c => c.FuneralCompanyDto, opt => opt.MapFrom(x => x.FuneralCompany))
                .ForMember(c => c.FuneralCompanyDtoId, opt => opt.MapFrom(x => x.FuneralCompanyId))
                .ForMember(c => c.FengShuiMasterDto, opt => opt.MapFrom(x => x.FengShuiMaster))
                .ForMember(c => c.FengShuiMasterDtoId, opt => opt.MapFrom(x => x.FengShuiMasterId))
                .ForMember(c => c.PlotDto, opt => opt.MapFrom(x => x.Plot))
                .ForMember(c => c.PlotDtoId, opt => opt.MapFrom(x => x.PlotId))
                .ForMember(c => c.ApplicantDto, opt => opt.MapFrom(x => x.Applicant))
                .ForMember(c => c.ApplicantDtoId, opt => opt.MapFrom(x => x.ApplicantId))
                .ForMember(c => c.DeceasedDto1, opt => opt.MapFrom(x => x.Deceased1))
                .ForMember(c => c.DeceasedDto1Id, opt => opt.MapFrom(x => x.Deceased1Id))
                .ForMember(c => c.DeceasedDto2, opt => opt.MapFrom(x => x.Deceased2))
                .ForMember(c => c.DeceasedDto2Id, opt => opt.MapFrom(x => x.Deceased2Id));
            CreateMap<PlotType, PlotTypeDto>();

            CreateMap<Niche, NicheDto>()
                .ForMember(c => c.ApplicantDto, opt => opt.MapFrom(x => x.Applicant))
                .ForMember(c => c.ApplicantDtoId, opt => opt.MapFrom(x => x.ApplicantId))
                .ForMember(c => c.NicheTypeDto, opt => opt.MapFrom(x => x.NicheType))
                .ForMember(c => c.NicheTypeDtoId, opt => opt.MapFrom(x => x.NicheTypeId))
                .ForMember(c => c.ColumbariumAreaDto, opt => opt.MapFrom(x => x.ColumbariumArea))
                .ForMember(c => c.ColumbariumAreaDtoId, opt => opt.MapFrom(x => x.ColumbariumAreaId));

            CreateMap<ColumbariumArea, ColumbariumAreaDto>()
                .ForMember(c => c.ColumbariumCentreDto, opt => opt.MapFrom(x => x.ColumbariumCentre))
                .ForMember(c => c.ColumbariumCentreDtoId, opt => opt.MapFrom(x => x.ColumbariumCentreId));

            CreateMap<ColumbariumCentre, ColumbariumCentreDto>()
                .ForMember(c => c.SiteDto, opt => opt.MapFrom(x => x.Site))
                .ForMember(c => c.SiteDtoId, opt => opt.MapFrom(x => x.SiteId));
            CreateMap<NicheType, NicheTypeDto>();
            CreateMap<ColumbariumItem, ColumbariumItemDto>()
                .ForMember(c => c.SubProductServiceDto, opt => opt.MapFrom(x => x.SubProductService))
                .ForMember(c => c.SubProductServiceDtoId, opt => opt.MapFrom(x => x.SubProductServiceId))
                .ForMember(c => c.ColumbariumCentreDto, opt => opt.MapFrom(x => x.ColumbariumCentre))
                .ForMember(c => c.ColumbariumCentreDtoId, opt => opt.MapFrom(x => x.ColumbariumCentreId));
            CreateMap<ColumbariumTransaction, ColumbariumTransactionDto>()
                .ForMember(c => c.ColumbariumItemDto, opt => opt.MapFrom(x => x.ColumbariumItem))
                .ForMember(c => c.ColumbariumItemDtoId, opt => opt.MapFrom(x => x.ColumbariumItemId))
                .ForMember(c => c.NicheDto, opt => opt.MapFrom(x => x.Niche))
                .ForMember(c => c.NicheDtoId, opt => opt.MapFrom(x => x.NicheId))
                .ForMember(c => c.FuneralCompanyDto, opt => opt.MapFrom(x => x.FuneralCompany))
                .ForMember(c => c.FuneralCompanyDtoId, opt => opt.MapFrom(x => x.FuneralCompanyId))
                .ForMember(c => c.ApplicantDto, opt => opt.MapFrom(x => x.Applicant))
                .ForMember(c => c.ApplicantDtoId, opt => opt.MapFrom(x => x.ApplicantId))
                .ForMember(c => c.DeceasedDto1, opt => opt.MapFrom(x => x.Deceased1))
                .ForMember(c => c.DeceasedDto1Id, opt => opt.MapFrom(x => x.Deceased1Id))
                .ForMember(c => c.DeceasedDto2, opt => opt.MapFrom(x => x.Deceased2))
                .ForMember(c => c.DeceasedDto2Id, opt => opt.MapFrom(x => x.Deceased2Id))
                .ForMember(c => c.ShiftedNicheDto, opt => opt.MapFrom(x => x.ShiftedNiche))
                .ForMember(c => c.ShiftedNicheDtoId, opt => opt.MapFrom(x => x.ShiftedNicheId))
                .ForMember(c => c.ShiftedColumbariumTransactionDto, opt => opt.MapFrom(x => x.ShiftedColumbariumTransaction))
                .ForMember(c => c.ShiftedColumbariumTransactionDtoAF, opt => opt.MapFrom(x => x.ShiftedColumbariumTransactionAF))
                .ForMember(c => c.TransferredApplicantDto, opt => opt.MapFrom(x => x.TransferredApplicant))
                .ForMember(c => c.TransferredApplicantDtoId, opt => opt.MapFrom(x => x.TransferredApplicantId))
                .ForMember(c => c.TransferredColumbariumTransactionDto, opt => opt.MapFrom(x => x.TransferredColumbariumTransaction))
                .ForMember(c => c.TransferredColumbariumTransactionDtoAF, opt => opt.MapFrom(x => x.TransferredColumbariumTransactionAF));

            CreateMap<AncestralTablet, AncestralTabletDto>()
                .ForMember(c => c.ApplicantDto, opt => opt.MapFrom(x => x.Applicant))
                .ForMember(c => c.ApplicantDtoId, opt => opt.MapFrom(x => x.ApplicantId))
                .ForMember(c => c.AncestralTabletAreaDto, opt => opt.MapFrom(x => x.AncestralTabletArea))
                .ForMember(c => c.AncestralTabletAreaDtoId, opt => opt.MapFrom(x => x.AncestralTabletAreaId));
            CreateMap<AncestralTabletArea, AncestralTabletAreaDto>()
                .ForMember(c => c.SiteDto, opt => opt.MapFrom(x => x.Site))
                .ForMember(c => c.SiteDtoId, opt => opt.MapFrom(x => x.SiteId));
            CreateMap<AncestralTabletItem, AncestralTabletItemDto>()
                .ForMember(c => c.SubProductServiceDto, opt => opt.MapFrom(x => x.SubProductService))
                .ForMember(c => c.SubProductServiceDtoId, opt => opt.MapFrom(x => x.SubProductServiceId))
                .ForMember(c => c.AncestralTabletAreaDto, opt => opt.MapFrom(x => x.AncestralTabletArea))
                .ForMember(c => c.AncestralTabletAreaDtoId, opt => opt.MapFrom(x => x.AncestralTabletAreaId));
            CreateMap<AncestralTabletTransaction, AncestralTabletTransactionDto>()
                .ForMember(c => c.AncestralTabletItemDto, opt => opt.MapFrom(x => x.AncestralTabletItem))
                .ForMember(c => c.AncestralTabletItemDtoId, opt => opt.MapFrom(x => x.AncestralTabletItemId))
                .ForMember(c => c.AncestralTabletItemDto, opt => opt.MapFrom(x => x.AncestralTabletItem))
                .ForMember(c => c.AncestralTabletItemDtoId, opt => opt.MapFrom(x => x.AncestralTabletItemId))
                .ForMember(c => c.AncestralTabletDto, opt => opt.MapFrom(x => x.AncestralTablet))
                .ForMember(c => c.AncestralTabletDtoId, opt => opt.MapFrom(x => x.AncestralTabletId))
                .ForMember(c => c.ApplicantDto, opt => opt.MapFrom(x => x.Applicant))
                .ForMember(c => c.ApplicantDtoId, opt => opt.MapFrom(x => x.ApplicantId))
                .ForMember(c => c.DeceasedDto, opt => opt.MapFrom(x => x.Deceased))
                .ForMember(c => c.DeceasedDtoId, opt => opt.MapFrom(x => x.DeceasedId))
                .ForMember(c => c.ShiftedAncestralTabletDto, opt => opt.MapFrom(x => x.ShiftedAncestralTablet))
                .ForMember(c => c.ShiftedAncestralTabletDtoId, opt => opt.MapFrom(x => x.ShiftedAncestralTabletId))
                .ForMember(c => c.ShiftedAncestralTabletTransactionDtoAF, opt => opt.MapFrom(x => x.ShiftedAncestralTabletTransactionAF));

            CreateMap<Cremation, CremationDto>()
                .ForMember(c => c.SiteDto, opt => opt.MapFrom(x => x.Site))
                .ForMember(c => c.SiteDtoId, opt => opt.MapFrom(x => x.SiteId));
            CreateMap<CremationItem, CremationItemDto>()
                .ForMember(c => c.SubProductServiceDto, opt => opt.MapFrom(x => x.SubProductService))
                .ForMember(c => c.SubProductServiceDtoId, opt => opt.MapFrom(x => x.SubProductServiceId))
                .ForMember(c => c.CremationDto, opt => opt.MapFrom(x => x.Cremation))
                .ForMember(c => c.CremationDtoId, opt => opt.MapFrom(x => x.CremationId));
            CreateMap<CremationTransaction, CremationTransactionDto>()
                .ForMember(c => c.CremationItemDto, opt => opt.MapFrom(x => x.CremationItem))
                .ForMember(c => c.CremationItemDtoId, opt => opt.MapFrom(x => x.CremationItemId))
                .ForMember(c => c.FuneralCompanyDto, opt => opt.MapFrom(x => x.FuneralCompany))
                .ForMember(c => c.FuneralCompanyDtoId, opt => opt.MapFrom(x => x.FuneralCompanyId))
                .ForMember(c => c.ApplicantDto, opt => opt.MapFrom(x => x.Applicant))
                .ForMember(c => c.ApplicantDtoId, opt => opt.MapFrom(x => x.ApplicantId))
                .ForMember(c => c.DeceasedDto, opt => opt.MapFrom(x => x.Deceased))
                .ForMember(c => c.DeceasedDtoId, opt => opt.MapFrom(x => x.DeceasedId));

            CreateMap<Invoice, InvoiceDto>();
            CreateMap<Receipt, ReceiptDto>()
                .ForMember(c => c.InvoiceDto, opt => opt.MapFrom(x => x.Invoice))
                .ForMember(c => c.InvoiceDtoIV, opt => opt.MapFrom(x => x.InvoiceIV));
            CreateMap<PaymentMethod, PaymentMethodDto>();


            // Dto to Domain
            CreateMap<ProductDto, Product>()
                .ForMember(c => c.Id, opt => opt.Ignore());

            CreateMap<SubProductServiceDto, SubProductService>()
                .ForMember(c => c.Id, opt => opt.Ignore())
                .ForMember(c => c.Product, opt => opt.Ignore())
                .ForMember(c => c.ProductId, opt => opt.MapFrom(x => x.ProductDtoId));

            CreateMap<CatalogDto, Catalog>()
                .ForMember(c => c.Id, opt => opt.Ignore())
                .ForMember(c => c.Product, opt => opt.Ignore())
                .ForMember(c => c.ProductId, opt => opt.MapFrom(x => x.ProductDtoId))
                .ForMember(c => c.Site, opt => opt.Ignore())
                .ForMember(c => c.SiteId, opt => opt.MapFrom(x => x.SiteDtoId));

            CreateMap<SiteDto, Site>()
                .ForMember(c => c.Id, opt => opt.Ignore())
                .ForMember(c => c.CreatedUtcTime, opt => opt.Ignore());


            CreateMap<GenderType, GenderTypeDto>();
            CreateMap<RelationshipType, RelationshipTypeDto>();
            CreateMap<MaritalType, MaritalTypeDto>();
            CreateMap<NationalityType, NationalityTypeDto>();
            CreateMap<ReligionType, ReligionTypeDto>();

            CreateMap<FengShuiMasterDto, FengShuiMaster>()
                .ForMember(c => c.Id, opt => opt.Ignore());
            CreateMap<FuneralCompanyDto, FuneralCompany>()
                .ForMember(c => c.Id, opt => opt.Ignore());
            CreateMap<CemeteryLandscapeCompanyDto, CemeteryLandscapeCompany>()
                .ForMember(c => c.Id, opt => opt.Ignore())
                .ForMember(c => c.CreatedUtcTime, opt => opt.Ignore());



            CreateMap<ApplicantDto, Applicant>()
                .ForMember(c => c.Id, opt => opt.Ignore())
                .ForMember(c => c.Site, opt => opt.Ignore())
                .ForMember(c => c.SiteId, opt => opt.MapFrom(x => x.SiteDtoId));
            CreateMap<DeceasedDto, Deceased>()
                .ForMember(c => c.Id, opt => opt.Ignore())
                .ForMember(c => c.GenderType, opt => opt.Ignore())
                .ForMember(c => c.GenderTypeId, opt => opt.MapFrom(x => x.GenderTypeDtoId))
                .ForMember(c => c.NationalityType, opt => opt.Ignore())
                .ForMember(c => c.NationalityTypeId, opt => opt.MapFrom(x => x.NationalityTypeDtoId))
                .ForMember(c => c.MaritalType, opt => opt.Ignore())
                .ForMember(c => c.MaritalTypeId, opt => opt.MapFrom(x => x.MaritalTypeDtoId))
                .ForMember(c => c.ReligionType, opt => opt.Ignore())
                .ForMember(c => c.ReligionTypeId, opt => opt.MapFrom(x => x.ReligionTypeDtoId))
                .ForMember(c => c.Niche, opt => opt.Ignore())
                .ForMember(c => c.Plot, opt => opt.Ignore())
                .ForMember(c => c.CreatedUtcTime, opt => opt.Ignore())
                .ForMember(c => c.ModifiedUtcTime, opt => opt.Ignore())
                .ForMember(c => c.DeletedUtcTime, opt => opt.Ignore());

            CreateMap<ApplicantDeceasedDto, ApplicantDeceased>()
                .ForMember(c => c.Id, opt => opt.Ignore())
                .ForMember(c => c.Applicant, opt => opt.Ignore())
                .ForMember(c => c.ApplicantId, opt => opt.MapFrom(x => x.ApplicantDtoId))
                .ForMember(c => c.Deceased, opt => opt.Ignore())
                .ForMember(c => c.DeceasedId, opt => opt.MapFrom(x => x.DeceasedDtoId))
                .ForMember(c => c.RelationshipType, opt => opt.Ignore())
                .ForMember(c => c.RelationshipTypeId, opt => opt.MapFrom(x => x.RelationshipTypeDtoId));
            
            CreateMap<CremationTransactionDto, CremationTransaction>()
                .ForMember(c => c.AF, opt => opt.Ignore());

            CreateMap<MiscellaneousDto, Miscellaneous>()
                .ForMember(c => c.Id, opt => opt.Ignore())
                .ForMember(c => c.Site, opt => opt.Ignore())
                .ForMember(c => c.SiteId, opt => opt.MapFrom(x => x.SiteDtoId))
                .ForMember(c => c.CreatedUtcTime, opt => opt.Ignore());
            CreateMap<MiscellaneousItemDto, MiscellaneousItem>()
                .ForMember(c => c.Id, opt => opt.Ignore())
                .ForMember(c => c.SubProductService, opt => opt.Ignore())
                .ForMember(c => c.SubProductServiceId, opt => opt.MapFrom(x => x.SubProductServiceDtoId))
                .ForMember(c => c.Miscellaneous, opt => opt.Ignore())
                .ForMember(c => c.MiscellaneousId, opt => opt.MapFrom(x => x.MiscellaneousDtoId))
                .ForMember(c => c.CreatedUtcTime, opt => opt.Ignore());

            CreateMap<MiscellaneousTransactionDto, MiscellaneousTransaction>()
                .ForMember(c => c.AF, opt => opt.Ignore())
                .ForMember(c => c.MiscellaneousItem, opt => opt.Ignore())
                .ForMember(c => c.MiscellaneousItemId, opt => opt.MapFrom(x => x.MiscellaneousItemDtoId))
                .ForMember(c => c.Applicant, opt => opt.Ignore())
                .ForMember(c => c.ApplicantId, opt => opt.MapFrom(x => x.ApplicantDtoId))
                .ForMember(c => c.CemeteryLandscapeCompany, opt => opt.Ignore())
                .ForMember(c => c.CemeteryLandscapeCompanyId, opt => opt.MapFrom(x => x.CemeteryLandscapeCompanyDtoId))
                .ForMember(c => c.CreatedUtcTime, opt => opt.Ignore());

            CreateMap<SpaceDto, Space>()
                .ForMember(c => c.Id, opt => opt.Ignore())
                .ForMember(c => c.Site, opt => opt.Ignore())
                .ForMember(c => c.SiteId, opt => opt.MapFrom(x => x.SiteDtoId));

            CreateMap<SpaceItemDto, SpaceItem>()
                .ForMember(c => c.Id, opt => opt.Ignore())
                .ForMember(c => c.SubProductService, opt => opt.Ignore())
                .ForMember(c => c.SubProductServiceId, opt => opt.MapFrom(x => x.SubProductServiceDtoId))
                .ForMember(c => c.Space, opt => opt.Ignore())
                .ForMember(c => c.SpaceId, opt => opt.MapFrom(x => x.SpaceDtoId))
                .ForMember(c => c.CreatedUtcTime, opt => opt.Ignore());

            CreateMap<SpaceTransactionDto, SpaceTransaction>()
                .ForMember(c => c.AF, opt => opt.Ignore())
                .ForMember(c => c.SpaceItem, opt => opt.Ignore())
                .ForMember(c => c.SpaceItemId, opt => opt.MapFrom(x => x.SpaceItemDtoId))
                .ForMember(c => c.FuneralCompany, opt => opt.Ignore())
                .ForMember(c => c.FuneralCompanyId, opt => opt.MapFrom(x => x.FuneralCompanyDtoId))
                .ForMember(c => c.Applicant, opt => opt.Ignore())
                .ForMember(c => c.ApplicantId, opt => opt.MapFrom(x => x.ApplicantDtoId))
                .ForMember(c => c.Deceased, opt => opt.Ignore())
                .ForMember(c => c.DeceasedId, opt => opt.MapFrom(x => x.DeceasedDtoId))
                .ForMember(c => c.CreatedUtcTime, opt => opt.Ignore());

            CreateMap<UrnDto, Urn>()
                .ForMember(c => c.Id, opt => opt.Ignore())
                .ForMember(c => c.Site, opt => opt.Ignore())
                .ForMember(c => c.SiteId, opt => opt.MapFrom(x => x.SiteDtoId))
                .ForMember(c => c.CreatedUtcTime, opt => opt.Ignore());

            CreateMap<UrnItemDto, UrnItem>()
                .ForMember(c => c.Id, opt => opt.Ignore())
                .ForMember(c => c.SubProductService, opt => opt.Ignore())
                .ForMember(c => c.SubProductServiceId, opt => opt.MapFrom(x => x.SubProductServiceDtoId))
                .ForMember(c => c.Urn, opt => opt.Ignore())
                .ForMember(c => c.UrnId, opt => opt.MapFrom(x => x.UrnDtoId))
                .ForMember(c => c.CreatedUtcTime, opt => opt.Ignore());

            CreateMap<UrnTransactionDto, UrnTransaction>()
                .ForMember(c => c.AF, opt => opt.Ignore())
                .ForMember(c => c.UrnItem, opt => opt.Ignore())
                .ForMember(c => c.UrnItemId, opt => opt.MapFrom(x => x.UrnItemDtoId))
                .ForMember(c => c.Applicant, opt => opt.Ignore())
                .ForMember(c => c.ApplicantId, opt => opt.MapFrom(x => x.ApplicantDtoId));

            CreateMap<PlotDto, Plot>()
                .ForMember(c => c.Id, opt => opt.Ignore())
                .ForMember(c => c.Applicant, opt => opt.Ignore())
                .ForMember(c => c.ApplicantId, opt => opt.MapFrom(x => x.ApplicantDtoId))
                .ForMember(c => c.PlotType, opt => opt.Ignore())
                .ForMember(c => c.PlotTypeId, opt => opt.MapFrom(x => x.PlotTypeDtoId))
                .ForMember(c => c.CemeteryArea, opt => opt.Ignore())
                .ForMember(c => c.CemeteryAreaId, opt => opt.MapFrom(x => x.CemeteryAreaDtoId));
            CreateMap<CemeteryAreaDto, CemeteryArea>()
                .ForMember(c => c.Id, opt => opt.Ignore())
                .ForMember(c => c.Site, opt => opt.Ignore())
                .ForMember(c => c.SiteId, opt => opt.MapFrom(x => x.SiteDtoId))
                .ForMember(c => c.CreatedUtcTime, opt => opt.Ignore());
            CreateMap<CemeteryItemDto, CemeteryItem>()
                .ForMember(c => c.Id, opt => opt.Ignore())
                .ForMember(c => c.Plot, opt => opt.Ignore())
                .ForMember(c => c.PlotId, opt => opt.MapFrom(x => x.PlotDtoId))
                .ForMember(c => c.SubProductService, opt => opt.Ignore())
                .ForMember(c => c.SubProductServiceId, opt => opt.MapFrom(x => x.SubProductServiceDtoId))
                .ForMember(c => c.CreatedUtcTime, opt => opt.Ignore());

            CreateMap<PlotTypeDto, PlotType>()
                .ForMember(c => c.Id, opt => opt.Ignore())
                .ForMember(c => c.CreatedUtcTime, opt => opt.Ignore());              
            CreateMap<CemeteryTransactionDto, CemeteryTransaction>()
                .ForMember(c => c.AF, opt => opt.Ignore())
                .ForMember(c => c.Applicant, opt => opt.Ignore())
                .ForMember(c => c.ApplicantId, opt => opt.MapFrom(x => x.ApplicantDtoId))
                .ForMember(c => c.Deceased1, opt => opt.MapFrom(x => x.DeceasedDto1))
                .ForMember(c => c.Deceased1Id, opt => opt.MapFrom(x => x.DeceasedDto1Id))
                .ForMember(c => c.Deceased2, opt => opt.MapFrom(x => x.DeceasedDto2))
                .ForMember(c => c.Deceased2Id, opt => opt.MapFrom(x => x.DeceasedDto2Id))
                .ForMember(c => c.Plot, opt => opt.Ignore())
                .ForMember(c => c.PlotId, opt => opt.MapFrom(x => x.PlotDtoId))
                .ForMember(c => c.FuneralCompany, opt => opt.Ignore())
                .ForMember(c => c.FuneralCompanyId, opt => opt.MapFrom(x => x.FuneralCompanyDtoId))
                .ForMember(c => c.FengShuiMaster, opt => opt.Ignore())
                .ForMember(c => c.FengShuiMasterId, opt => opt.MapFrom(x => x.FengShuiMasterDtoId))
                .ForMember(c => c.CemeteryItem, opt => opt.Ignore())
                .ForMember(c => c.CemeteryItemId, opt => opt.MapFrom(x => x.CemeteryItemDtoId))
                .ForMember(c => c.CreatedUtcTime, opt => opt.Ignore());

            CreateMap<NicheDto, Niche>()
                .ForMember(c => c.Id, opt => opt.Ignore())
                .ForMember(c => c.NicheType, opt => opt.Ignore())
                .ForMember(c => c.NicheTypeId, opt => opt.MapFrom(x => x.NicheTypeDtoId))
                .ForMember(c => c.ColumbariumArea, opt => opt.Ignore())
                .ForMember(c => c.ColumbariumAreaId, opt => opt.MapFrom(x => x.ColumbariumAreaDtoId));

            CreateMap<ColumbariumAreaDto, ColumbariumArea>()
                .ForMember(c => c.Id, opt => opt.Ignore())
                .ForMember(c => c.ColumbariumCentre, opt => opt.Ignore())
                .ForMember(c => c.ColumbariumCentreId, opt => opt.MapFrom(x => x.ColumbariumCentreDtoId))
                .ForMember(c => c.CreatedUtcTime, opt => opt.Ignore());
            CreateMap<ColumbariumCentreDto, ColumbariumCentre>()
                .ForMember(c => c.Id, opt => opt.Ignore())
                .ForMember(c => c.Site, opt => opt.Ignore())
                .ForMember(c => c.SiteId, opt => opt.MapFrom(x => x.SiteDtoId))
                .ForMember(c => c.CreatedUtcTime, opt => opt.Ignore());
            CreateMap<ColumbariumItemDto, ColumbariumItem>()
                .ForMember(c => c.Id, opt => opt.Ignore())
                .ForMember(c => c.SubProductService, opt => opt.Ignore())
                .ForMember(c => c.SubProductServiceId, opt => opt.MapFrom(x => x.SubProductServiceDtoId))
                .ForMember(c => c.ColumbariumCentre, opt => opt.Ignore())
                .ForMember(c => c.ColumbariumCentreId, opt => opt.MapFrom(x => x.ColumbariumCentreDtoId))
                .ForMember(c => c.CreatedUtcTime, opt => opt.Ignore());
            CreateMap<NicheTypeDto, NicheType>()
                .ForMember(c => c.Id, opt => opt.Ignore());
            CreateMap<ColumbariumTransactionDto, ColumbariumTransaction>()
                .ForMember(c => c.AF, opt => opt.Ignore())
                .ForMember(c => c.ColumbariumItem, opt => opt.Ignore())
                .ForMember(c => c.ColumbariumItemId, opt => opt.MapFrom(x => x.ColumbariumItemDtoId))
                .ForMember(c => c.Niche, opt => opt.Ignore())
                .ForMember(c => c.NicheId, opt => opt.MapFrom(x => x.NicheDtoId))
                .ForMember(c => c.FuneralCompany, opt => opt.Ignore())
                .ForMember(c => c.FuneralCompanyId, opt => opt.MapFrom(x => x.FuneralCompanyDtoId))
                .ForMember(c => c.Applicant, opt => opt.Ignore())
                .ForMember(c => c.ApplicantId, opt => opt.MapFrom(x => x.ApplicantDtoId))
                .ForMember(c => c.Deceased1, opt => opt.Ignore())
                .ForMember(c => c.Deceased1Id, opt => opt.MapFrom(x => x.DeceasedDto1Id))
                .ForMember(c => c.Deceased2, opt => opt.Ignore())
                .ForMember(c => c.Deceased2Id, opt => opt.MapFrom(x => x.DeceasedDto2Id))
                .ForMember(c => c.ShiftedNiche, opt => opt.Ignore())
                .ForMember(c => c.ShiftedNicheId, opt => opt.MapFrom(x => x.ShiftedNicheDtoId))
                .ForMember(c => c.ShiftedColumbariumTransaction, opt => opt.Ignore())
                .ForMember(c => c.ShiftedColumbariumTransactionAF, opt => opt.MapFrom(x => x.ShiftedColumbariumTransactionDtoAF))
                .ForMember(c => c.TransferredApplicant, opt => opt.Ignore())
                .ForMember(c => c.TransferredApplicantId, opt => opt.MapFrom(x => x.TransferredApplicantDtoId))
                .ForMember(c => c.TransferredColumbariumTransaction, opt => opt.Ignore())
                .ForMember(c => c.TransferredColumbariumTransactionAF, opt => opt.MapFrom(x => x.TransferredColumbariumTransactionDtoAF))
                .ForMember(c => c.CreatedUtcTime, opt => opt.Ignore());


            CreateMap<AncestralTabletDto, AncestralTablet>()
                .ForMember(c => c.Id, opt => opt.Ignore())
                .ForMember(c => c.Applicant, opt => opt.Ignore())
                .ForMember(c => c.ApplicantId, opt => opt.MapFrom(x => x.ApplicantDtoId))
                .ForMember(c => c.AncestralTabletArea, opt => opt.Ignore())
                .ForMember(c => c.AncestralTabletAreaId, opt => opt.MapFrom(x => x.AncestralTabletAreaDtoId));
            CreateMap<AncestralTabletAreaDto, AncestralTabletArea>()
                .ForMember(c => c.Id, opt => opt.Ignore())
                .ForMember(c => c.Site, opt => opt.Ignore())
                .ForMember(c => c.SiteId, opt => opt.MapFrom(x => x.SiteDtoId));
            CreateMap<AncestralTabletItemDto, AncestralTabletItem>()
                .ForMember(c => c.Id, opt => opt.Ignore())
                .ForMember(c => c.SubProductService, opt => opt.Ignore())
                .ForMember(c => c.SubProductServiceId, opt => opt.MapFrom(x => x.SubProductServiceDtoId))
                .ForMember(c => c.AncestralTabletArea, opt => opt.Ignore())
                .ForMember(c => c.AncestralTabletAreaId, opt => opt.MapFrom(x => x.AncestralTabletAreaDtoId))
                .ForMember(c => c.CreatedUtcTime, opt => opt.Ignore());
            CreateMap<AncestralTabletTransactionDto, AncestralTabletTransaction>()
                .ForMember(c => c.AF, opt => opt.Ignore())
                .ForMember(c => c.AncestralTabletItem, opt => opt.Ignore())
                .ForMember(c => c.AncestralTabletItemId, opt => opt.MapFrom(x => x.AncestralTabletItemDtoId))
                .ForMember(c => c.AncestralTablet, opt => opt.Ignore())
                .ForMember(c => c.AncestralTabletId, opt => opt.MapFrom(x => x.AncestralTabletDtoId))
                .ForMember(c => c.Applicant, opt => opt.Ignore())
                .ForMember(c => c.ApplicantId, opt => opt.MapFrom(x => x.ApplicantDtoId))
                .ForMember(c => c.Deceased, opt => opt.Ignore())
                .ForMember(c => c.DeceasedId, opt => opt.MapFrom(x => x.DeceasedDtoId))
                .ForMember(c => c.ShiftedAncestralTablet, opt => opt.Ignore())
                .ForMember(c => c.ShiftedAncestralTabletId, opt => opt.MapFrom(x => x.ShiftedAncestralTabletDtoId))
                .ForMember(c => c.ShiftedAncestralTabletTransactionAF, opt => opt.MapFrom(x => x.ShiftedAncestralTabletTransactionDtoAF))
                .ForMember(c => c.CreatedUtcTime, opt => opt.Ignore())
                .ForMember(c => c.ModifiedUtcTime, opt => opt.Ignore())
                .ForMember(c => c.DeletedUtcTime, opt => opt.Ignore());

            CreateMap<CremationDto, Cremation>()
                .ForMember(c => c.Id, opt => opt.Ignore())
                .ForMember(c => c.Site, opt => opt.Ignore())
                .ForMember(c => c.SiteId, opt => opt.MapFrom(x => x.SiteDtoId));
            CreateMap<CremationItemDto, CremationItem>()
                .ForMember(c => c.Id, opt => opt.Ignore())
                .ForMember(c => c.SubProductService, opt => opt.Ignore())
                .ForMember(c => c.SubProductServiceId, opt => opt.MapFrom(x => x.SubProductServiceDtoId))
                .ForMember(c => c.Cremation, opt => opt.Ignore())
                .ForMember(c => c.CremationId, opt => opt.MapFrom(x => x.CremationDtoId))
                .ForMember(i => i.CreatedUtcTime, opt => opt.Ignore());

            CreateMap<CremationTransactionDto, CremationTransaction>()
                .ForMember(c => c.AF, opt => opt.Ignore())
                .ForMember(c => c.CremationItem, opt => opt.Ignore())
                .ForMember(c => c.CremationItemId, opt => opt.MapFrom(x => x.CremationItemDtoId))
                .ForMember(c => c.FuneralCompany, opt => opt.Ignore())
                .ForMember(c => c.FuneralCompanyId, opt => opt.MapFrom(x => x.FuneralCompanyDtoId))
                .ForMember(c => c.Applicant, opt => opt.Ignore())
                .ForMember(c => c.ApplicantId, opt => opt.MapFrom(x => x.ApplicantDtoId))
                .ForMember(c => c.Deceased, opt => opt.Ignore())
                .ForMember(c => c.DeceasedId, opt => opt.MapFrom(x => x.DeceasedDtoId));

            CreateMap<InvoiceDto, Invoice>()
                .ForMember(i => i.IV, opt => opt.Ignore())
                .ForMember(i => i.AncestralTabletTransaction, opt => opt.Ignore())
                .ForMember(i => i.CremationTransaction, opt => opt.Ignore())
                .ForMember(i => i.MiscellaneousTransaction, opt => opt.Ignore())
                .ForMember(i => i.ColumbariumTransaction, opt => opt.Ignore())
                .ForMember(i => i.SpaceTransaction, opt => opt.Ignore())
                .ForMember(i => i.UrnTransaction, opt => opt.Ignore())
                .ForMember(i => i.CreatedUtcTime, opt => opt.Ignore());

            CreateMap<ReceiptDto, Receipt>()
                .ForMember(i => i.RE, opt => opt.Ignore())
                .ForMember(i => i.Invoice, opt => opt.Ignore())
                .ForMember(i => i.InvoiceIV, opt => opt.MapFrom(x => x.InvoiceDtoIV))
                .ForMember(i => i.PaymentMethod, opt => opt.Ignore())
                .ForMember(i => i.AncestralTabletTransaction, opt => opt.Ignore())
                .ForMember(i => i.CremationTransaction, opt => opt.Ignore())
                .ForMember(i => i.MiscellaneousTransaction, opt => opt.Ignore())
                .ForMember(i => i.ColumbariumTransaction, opt => opt.Ignore())
                .ForMember(i => i.SpaceTransaction, opt => opt.Ignore())
                .ForMember(i => i.UrnTransaction, opt => opt.Ignore())
                .ForMember(i => i.CreatedUtcTime, opt => opt.Ignore());

            CreateMap<PaymentMethodDto, PaymentMethod>()
                .ForMember(pm => pm.Id, opt => opt.Ignore());
        }
    }
}