using System.Collections.Generic;
using Memorial.Core.Dtos;

namespace Memorial.ViewModels
{
    public class SpaceIndexesViewModel
    {
        public IEnumerable<SpaceDto> SpaceDtos { get; set; }

        public int ApplicantId { get; set; }

        public SiteDto siteDto { get; set; }
    }
}