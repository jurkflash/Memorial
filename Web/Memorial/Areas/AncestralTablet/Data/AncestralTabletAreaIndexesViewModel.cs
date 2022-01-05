using System.Collections.Generic;
using Memorial.Core.Dtos;

namespace Memorial.ViewModels
{
    public class AncestralTabletAreaIndexesViewModel
    {
        public IEnumerable<AncestralTabletAreaDto> AncestralTabletAreaDtos { get; set; }

        public int ApplicantId { get; set; }

        public SiteDto SiteDto { get; set; }
    }
}