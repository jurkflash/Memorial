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

            CreateMap<Applicant, ApplicantDto>();
            CreateMap<Deceased, DeceasedDto>();
            CreateMap<Deceased, DeceasedBriefDto>();
            CreateMap<FuneralCompany, FuneralCompanyDto>();
            CreateMap<CremationTransaction, CremationTransactionDto>();

            CreateMap<Miscellaneous, MiscellaneousDto>();
            CreateMap<MiscellaneousItem, MiscellaneousItemDto>();
            CreateMap<MiscellaneousTransaction, MiscellaneousTransactionDto>();

            CreateMap<Space, SpaceDto>();
            CreateMap<SpaceItem, SpaceItemDto>();
            CreateMap<SpaceTransaction, SpaceTransactionDto>();

            CreateMap<Urn, UrnDto>();
            CreateMap<UrnItem, UrnItemDto>();
            CreateMap<UrnTransaction, UrnTransactionDto>();

            CreateMap<Quadrangle, QuadrangleDto>();
            CreateMap<QuadrangleArea, QuadrangleAreaDto>();
            CreateMap<QuadrangleCentre, QuadrangleCentreDto>();
            CreateMap<QuadrangleItem, QuadrangleItemDto>();
            CreateMap<QuadrangleTransaction, QuadrangleTransactionDto>();

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

            CreateMap<ApplicantDto, Applicant>()
                .ForMember(c => c.Id, opt => opt.Ignore());
            CreateMap<DeceasedDto, Deceased>()
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
                .ForMember(c => c.AF, opt => opt.Ignore());

            CreateMap<SpaceDto, Space>()
                .ForMember(c => c.Id, opt => opt.Ignore());
            CreateMap<SpaceItemDto, SpaceItem>()
                .ForMember(c => c.Id, opt => opt.Ignore());
            CreateMap<SpaceTransactionDto, SpaceTransaction>()
                .ForMember(c => c.AF, opt => opt.Ignore());

            CreateMap<UrnDto, Urn>()
                .ForMember(c => c.Id, opt => opt.Ignore());
            CreateMap<UrnItemDto, UrnItem>()
                .ForMember(c => c.Id, opt => opt.Ignore());
            CreateMap<UrnTransactionDto, UrnTransaction>()
                .ForMember(c => c.AF, opt => opt.Ignore());

            CreateMap<QuadrangleDto, Quadrangle>()
                .ForMember(c => c.Id, opt => opt.Ignore());
            CreateMap<QuadrangleAreaDto, QuadrangleArea>()
                .ForMember(c => c.Id, opt => opt.Ignore());
            CreateMap<QuadrangleCentreDto, QuadrangleCentre>()
                .ForMember(c => c.Id, opt => opt.Ignore());
            CreateMap<QuadrangleItemDto, QuadrangleItem>()
                .ForMember(c => c.Id, opt => opt.Ignore());
            CreateMap<QuadrangleTransactionDto, QuadrangleTransaction>()
                .ForMember(c => c.AF, opt => opt.Ignore());

            CreateMap<AncestorDto, Ancestor>()
                .ForMember(c => c.Id, opt => opt.Ignore());
            CreateMap<AncestorAreaDto, AncestorArea>()
                .ForMember(c => c.Id, opt => opt.Ignore());
            CreateMap<AncestorItemDto, AncestorItem>()
                .ForMember(c => c.Id, opt => opt.Ignore());
            CreateMap<AncestorTransactionDto, AncestorTransaction>()
                .ForMember(c => c.AF, opt => opt.Ignore());

            CreateMap<CremationDto, Cremation>()
                .ForMember(c => c.Id, opt => opt.Ignore());
            CreateMap<CremationItemDto, CremationItem>()
                .ForMember(c => c.Id, opt => opt.Ignore());
            CreateMap<CremationTransactionDto, CremationTransaction>()
                .ForMember(c => c.AF, opt => opt.Ignore());

            CreateMap<InvoiceDto, Invoice>()
                .ForMember(i => i.IV, opt => opt.Ignore());
            CreateMap<ReceiptDto, Receipt>()
                .ForMember(i => i.RE, opt => opt.Ignore());
            CreateMap<PaymentMethodDto, PaymentMethod>()
                .ForMember(pm => pm.Id, opt => opt.Ignore());
        }
    }
}