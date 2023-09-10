using AutoMapper;
using Memorial.Core.Dtos;
using Memorial.Core.Domain;

namespace Memorial.App_Start
{
    public class DtoToDomain : Profile
    {
        public DtoToDomain()
        {
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