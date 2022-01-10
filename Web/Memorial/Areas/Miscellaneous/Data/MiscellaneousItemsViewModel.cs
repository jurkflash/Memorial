using System.Collections.Generic;
using Memorial.Core.Dtos;

namespace Memorial.ViewModels
{
    public class MiscellaneousItemsViewModel
    {
        public IEnumerable<MiscellaneousItemDto> MiscellaneousItemDtos { get; set; }
        public int? ApplicantId { get; set; }
        public SiteDto SiteDto { get; set; }
    }
}