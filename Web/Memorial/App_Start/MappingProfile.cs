using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using Memorial.Core.Dtos;
using Memorial.Core.Domain;

namespace Memorial.App_Start
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Domain to Dto
            CreateMap<Site, SiteDto>();

            CreateMap<GenderType, GenderTypeDto>();
            CreateMap<RelationshipType, RelationshipTypeDto>();
            CreateMap<MaritalType, MaritalTypeDto>();
            CreateMap<NationalityType, NationalityTypeDto>();
            CreateMap<ReligionType, ReligionTypeDto>();

            CreateMap<Applicant, ApplicantDto>();
            CreateMap<Deceased, DeceasedDto>();
            CreateMap<Deceased, DeceasedBriefDto>();
            CreateMap<ApplicantDeceased, ApplicantDeceasedDto>();
            CreateMap<FuneralCompany, FuneralCompanyDto>();
            CreateMap<CremationTransaction, CremationTransactionDto>();

            CreateMap<Miscellaneous, MiscellaneousDto>();
            CreateMap<MiscellaneousItem, MiscellaneousItemDto>();
            CreateMap<MiscellaneousTransaction, MiscellaneousTransactionDto>()
                .ForMember(c => c.MiscellaneousItemDto, opt => opt.MapFrom(x => x.MiscellaneousItem))
                .ForMember(c => c.MiscellaneousItemDtoId, opt => opt.MapFrom(x => x.MiscellaneousItemId))
                .ForMember(c => c.ApplicantDto, opt => opt.MapFrom(x => x.Applicant))
                .ForMember(c => c.ApplicantDtoId, opt => opt.MapFrom(x => x.ApplicantId))
                .ForMember(c => c.PlotLandscapeCompanyDto, opt => opt.MapFrom(x => x.PlotLandscapeCompany))
                .ForMember(c => c.PlotLandscapeCompanyDtoId, opt => opt.MapFrom(x => x.PlotLandscapeCompanyId));

            CreateMap<Space, SpaceDto>();
            CreateMap<SpaceItem, SpaceItemDto>();
            CreateMap<SpaceTransaction, SpaceTransactionDto>();

            CreateMap<Urn, UrnDto>();
            CreateMap<UrnItem, UrnItemDto>();
            CreateMap<UrnTransaction, UrnTransactionDto>();

            CreateMap<Plot, PlotDto>()
                .ForMember(c => c.ApplicantDto, opt => opt.MapFrom(x => x.Applicant))
                .ForMember(c => c.ApplicantDtoId, opt => opt.MapFrom(x => x.ApplicantId))
                .ForMember(c => c.PlotTypeDto, opt => opt.MapFrom(x => x.PlotType))
                .ForMember(c => c.PlotTypeDtoId, opt => opt.MapFrom(x => x.PlotTypeId));
            CreateMap<PlotArea, PlotAreaDto>();
            CreateMap<PlotItem, PlotItemDto>();
            CreateMap<PlotLandscapeCompany, PlotLandscapeCompanyDto>();
            CreateMap<CemeteryTransaction, CemeteryTransactionDto>()
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
                .ForMember(c => c.NicheTypeDto, opt => opt.MapFrom(x => x.NicheType))
                .ForMember(c => c.NicheTypeDtoId, opt => opt.MapFrom(x => x.NicheTypeId))
                .ForMember(c => c.ColumbariumAreaDto, opt => opt.MapFrom(x => x.ColumbariumArea))
                .ForMember(c => c.ColumbariumAreaDtoId, opt => opt.MapFrom(x => x.ColumbariumAreaId));

            CreateMap<ColumbariumArea, ColumbariumAreaDto>()
                .ForMember(c => c.ColumbariumCentreDto, opt => opt.MapFrom(x => x.ColumbariumCentre))
                .ForMember(c => c.ColumbariumCentreDtoId, opt => opt.MapFrom(x => x.ColumbariumCentreId));

            CreateMap<ColumbariumCentre, ColumbariumCentreDto>();
            CreateMap<NicheType, NicheTypeDto>();
            CreateMap<ColumbariumItem, ColumbariumItemDto>();
            CreateMap<ColumbariumTransaction, ColumbariumTransactionDto>();

            CreateMap<Ancestor, AncestorDto>();
            CreateMap<AncestorArea, AncestorAreaDto>();
            CreateMap<AncestorItem, AncestorItemDto>();
            CreateMap<AncestorTransaction, AncestorTransactionDto>();

            CreateMap<Cremation, CremationDto>();
            CreateMap<CremationItem, CremationItemDto>();
            CreateMap<CremationTransaction, CremationTransactionDto>();

            CreateMap<Invoice, InvoiceDto>();
            CreateMap<Receipt, ReceiptDto>();
            CreateMap<PaymentMethod, PaymentMethodDto>();


            // Dto to Domain
            CreateMap<SiteDto, Site>()
                .ForMember(c => c.Id, opt => opt.Ignore());


            CreateMap<GenderType, GenderTypeDto>();
            CreateMap<RelationshipType, RelationshipTypeDto>();
            CreateMap<MaritalType, MaritalTypeDto>();
            CreateMap<NationalityType, NationalityTypeDto>();
            CreateMap<ReligionType, ReligionTypeDto>();


            CreateMap<ApplicantDto, Applicant>()
                .ForMember(c => c.Id, opt => opt.Ignore());
            CreateMap<DeceasedDto, Deceased>()
                .ForMember(c => c.Id, opt => opt.Ignore())
                .ForMember(c => c.GenderType, opt => opt.Ignore())
                .ForMember(c => c.NationalityType, opt => opt.Ignore())
                .ForMember(c => c.MaritalType, opt => opt.Ignore())
                .ForMember(c => c.ReligionType, opt => opt.Ignore())
                .ForMember(c => c.Niche, opt => opt.Ignore())
                .ForMember(c => c.Plot, opt => opt.Ignore())
                .ForMember(c => c.CreateDate, opt => opt.Ignore())
                .ForMember(c => c.ModifyDate, opt => opt.Ignore())
                .ForMember(c => c.DeleteDate, opt => opt.Ignore());

            CreateMap<ApplicantDeceasedDto, ApplicantDeceased>()
                .ForMember(c => c.Id, opt => opt.Ignore());
            CreateMap<FuneralCompanyDto, FuneralCompany>()
                .ForMember(c => c.Id, opt => opt.Ignore());
            CreateMap<CremationTransactionDto, CremationTransaction>()
                .ForMember(c => c.AF, opt => opt.Ignore());

            CreateMap<MiscellaneousDto, Miscellaneous>()
                .ForMember(c => c.Id, opt => opt.Ignore());
            CreateMap<MiscellaneousItemDto, MiscellaneousItem>()
                .ForMember(c => c.Id, opt => opt.Ignore());
            CreateMap<MiscellaneousTransactionDto, MiscellaneousTransaction>()
                .ForMember(c => c.AF, opt => opt.Ignore())
                .ForMember(c => c.MiscellaneousItem, opt => opt.Ignore())
                .ForMember(c => c.MiscellaneousItemId, opt => opt.MapFrom(x => x.MiscellaneousItemDtoId))
                .ForMember(c => c.Applicant, opt => opt.Ignore())
                .ForMember(c => c.ApplicantId, opt => opt.MapFrom(x => x.ApplicantDtoId))
                .ForMember(c => c.PlotLandscapeCompany, opt => opt.Ignore())
                .ForMember(c => c.PlotLandscapeCompanyId, opt => opt.MapFrom(x => x.PlotLandscapeCompanyDtoId))
                .ForMember(c => c.CreateDate, opt => opt.Ignore());

            CreateMap<SpaceDto, Space>()
                .ForMember(c => c.Id, opt => opt.Ignore());
            CreateMap<SpaceItemDto, SpaceItem>()
                .ForMember(c => c.Id, opt => opt.Ignore());
            CreateMap<SpaceTransactionDto, SpaceTransaction>()
                .ForMember(c => c.AF, opt => opt.Ignore())
                .ForMember(c => c.CreateDate, opt => opt.Ignore());

            CreateMap<UrnDto, Urn>()
                .ForMember(c => c.Id, opt => opt.Ignore());
            CreateMap<UrnItemDto, UrnItem>()
                .ForMember(c => c.Id, opt => opt.Ignore());
            CreateMap<UrnTransactionDto, UrnTransaction>()
                .ForMember(c => c.AF, opt => opt.Ignore());

            CreateMap<PlotDto, Plot>()
                .ForMember(c => c.Id, opt => opt.Ignore())
                .ForMember(c => c.Applicant, opt => opt.MapFrom(x => x.ApplicantDto))
                .ForMember(c => c.ApplicantId, opt => opt.MapFrom(x => x.ApplicantDtoId))
                .ForMember(c => c.PlotType, opt => opt.MapFrom(x => x.PlotTypeDto))
                .ForMember(c => c.PlotTypeId, opt => opt.MapFrom(x => x.PlotTypeDtoId));
            CreateMap<PlotAreaDto, PlotArea>()
                .ForMember(c => c.Id, opt => opt.Ignore());
            CreateMap<PlotItemDto, PlotItem>()
                .ForMember(c => c.Id, opt => opt.Ignore());
            CreateMap<PlotTypeDto, PlotType>()
                .ForMember(c => c.Id, opt => opt.Ignore());
            CreateMap<PlotLandscapeCompanyDto, PlotLandscapeCompany>()
                .ForMember(c => c.Id, opt => opt.Ignore());
            CreateMap<CemeteryTransactionDto, CemeteryTransaction>()
                .ForMember(c => c.AF, opt => opt.Ignore())
                .ForMember(c => c.ApplicantId, opt => opt.MapFrom(x => x.ApplicantDtoId))
                .ForMember(c => c.Deceased1, opt => opt.MapFrom(x => x.DeceasedDto1))
                .ForMember(c => c.Deceased1Id, opt => opt.MapFrom(x => x.DeceasedDto1Id))
                .ForMember(c => c.Deceased2, opt => opt.MapFrom(x => x.DeceasedDto2))
                .ForMember(c => c.Deceased2Id, opt => opt.MapFrom(x => x.DeceasedDto2Id))
                .ForMember(c => c.PlotId, opt => opt.MapFrom(x => x.PlotDtoId))
                .ForMember(c => c.CreateDate, opt => opt.Ignore());

            CreateMap<NicheDto, Niche>()
                .ForMember(c => c.Id, opt => opt.Ignore());
            CreateMap<ColumbariumAreaDto, ColumbariumArea>()
                .ForMember(c => c.Id, opt => opt.Ignore());
            CreateMap<ColumbariumCentreDto, ColumbariumCentre>()
                .ForMember(c => c.Id, opt => opt.Ignore());
            CreateMap<ColumbariumItemDto, ColumbariumItem>()
                .ForMember(c => c.Id, opt => opt.Ignore());
            CreateMap<NicheTypeDto, NicheType>()
                .ForMember(c => c.Id, opt => opt.Ignore());
            CreateMap<ColumbariumTransactionDto, ColumbariumTransaction>()
                .ForMember(c => c.AF, opt => opt.Ignore())
                .ForMember(c => c.ColumbariumItem, opt => opt.Ignore())
                .ForMember(c => c.Niche, opt => opt.Ignore())
                .ForMember(c => c.FuneralCompany, opt => opt.Ignore())
                .ForMember(c => c.Applicant, opt => opt.Ignore())
                .ForMember(c => c.Deceased1, opt => opt.Ignore())
                .ForMember(c => c.Deceased2, opt => opt.Ignore())
                .ForMember(c => c.ShiftedNiche, opt => opt.Ignore())
                .ForMember(c => c.ShiftedColumbariumTransaction, opt => opt.Ignore())
                .ForMember(c => c.TransferredApplicant, opt => opt.Ignore())
                .ForMember(c => c.TransferredColumbariumTransaction, opt => opt.Ignore())
                .ForMember(c => c.CreateDate, opt => opt.Ignore());

            CreateMap<AncestorDto, Ancestor>()
                .ForMember(c => c.Id, opt => opt.Ignore());
            CreateMap<AncestorAreaDto, AncestorArea>()
                .ForMember(c => c.Id, opt => opt.Ignore());
            CreateMap<AncestorItemDto, AncestorItem>()
                .ForMember(c => c.Id, opt => opt.Ignore());
            CreateMap<AncestorTransactionDto, AncestorTransaction>()
                .ForMember(c => c.AF, opt => opt.Ignore())
                .ForMember(c => c.AncestorItem, opt => opt.Ignore())
                .ForMember(c => c.Ancestor, opt => opt.Ignore())
                .ForMember(c => c.Applicant, opt => opt.Ignore())
                .ForMember(c => c.Deceased, opt => opt.Ignore())
                .ForMember(c => c.CreateDate, opt => opt.Ignore())
                .ForMember(c => c.ModifyDate, opt => opt.Ignore())
                .ForMember(c => c.DeleteDate, opt => opt.Ignore());

            CreateMap<CremationDto, Cremation>()
                .ForMember(c => c.Id, opt => opt.Ignore());
            CreateMap<CremationItemDto, CremationItem>()
                .ForMember(c => c.Id, opt => opt.Ignore());
            CreateMap<CremationTransactionDto, CremationTransaction>()
                .ForMember(c => c.AF, opt => opt.Ignore());

            CreateMap<InvoiceDto, Invoice>()
                .ForMember(i => i.IV, opt => opt.Ignore())
                .ForMember(i => i.AncestorTransaction, opt => opt.Ignore())
                .ForMember(i => i.CremationTransaction, opt => opt.Ignore())
                .ForMember(i => i.MiscellaneousTransaction, opt => opt.Ignore())
                .ForMember(i => i.ColumbariumTransaction, opt => opt.Ignore())
                .ForMember(i => i.SpaceTransaction, opt => opt.Ignore())
                .ForMember(i => i.UrnTransaction, opt => opt.Ignore())
                .ForMember(i => i.CreateDate, opt => opt.Ignore());

            CreateMap<ReceiptDto, Receipt>()
                .ForMember(i => i.RE, opt => opt.Ignore())
                .ForMember(i => i.Invoice, opt => opt.Ignore())
                .ForMember(i => i.PaymentMethod, opt => opt.Ignore())
                .ForMember(i => i.AncestorTransaction, opt => opt.Ignore())
                .ForMember(i => i.CremationTransaction, opt => opt.Ignore())
                .ForMember(i => i.MiscellaneousTransaction, opt => opt.Ignore())
                .ForMember(i => i.ColumbariumTransaction, opt => opt.Ignore())
                .ForMember(i => i.SpaceTransaction, opt => opt.Ignore())
                .ForMember(i => i.UrnTransaction, opt => opt.Ignore())
                .ForMember(i => i.CreateDate, opt => opt.Ignore());

            CreateMap<PaymentMethodDto, PaymentMethod>()
                .ForMember(pm => pm.Id, opt => opt.Ignore());
        }
    }
}