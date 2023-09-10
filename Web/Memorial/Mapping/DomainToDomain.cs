using AutoMapper;
using Memorial.Core.Domain;

namespace Memorial.App_Start
{
    public class DomainToDomain : Profile
    {
        public DomainToDomain()
        {
            CreateMap<SpaceTransaction, SpaceBooked>()
                .ForMember(c => c.AF, opt => opt.MapFrom(x => x.AF))
                .ForMember(c => c.FromDate, opt => opt.MapFrom(x => x.FromDate))
                .ForMember(c => c.ToDate, opt => opt.MapFrom(x => x.ToDate))
                .ForMember(c => c.SpaceName, opt => opt.MapFrom(x => x.SpaceItem.Space.Name))
                .ForMember(c => c.SpaceColorCode, opt => opt.MapFrom(x => x.SpaceItem.Space.ColorCode))
                .ForMember(c => c.TransactionRemark, opt => opt.MapFrom(x => x.Remark));
        }
    }
}