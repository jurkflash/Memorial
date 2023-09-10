using AutoMapper;
using Memorial.Core.Dtos;
using Memorial.Core.Domain;

namespace Memorial.App_Start
{
    public class DomainToDto : Profile
    {
        public DomainToDto()
        {
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
            CreateMap<ApplicantDeceasedFlatten, ApplicantDeceasedFlattenDto>();
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
        }
    }
}