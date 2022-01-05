using System.Collections.Generic;
using Memorial.Core.Dtos;

namespace Memorial.ViewModels
{
    public class AncestralTabletIndexesViewModel
    {
        public IEnumerable<AncestralTabletDto> AncestralTabletDtos { get; set; }

        public IDictionary<byte, IEnumerable<byte>> Positions { get; set; }

        public int ApplicantId { get; set; }

        public SiteDto SiteDto { get; set; }
    }
}