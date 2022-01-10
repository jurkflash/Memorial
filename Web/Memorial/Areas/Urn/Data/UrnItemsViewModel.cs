using System.Collections.Generic;
using Memorial.Core.Dtos;

namespace Memorial.ViewModels
{
    public class UrnItemsViewModel
    {
        public IEnumerable<UrnItemDto> UrnItemDtos { get; set; }

        public int ApplicantId { get; set; }

        public SiteDto SiteDto { get; set; }
    }
}