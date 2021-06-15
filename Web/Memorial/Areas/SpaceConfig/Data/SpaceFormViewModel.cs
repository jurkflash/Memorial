using System.Collections.Generic;
using Memorial.Core.Dtos;

namespace Memorial.ViewModels
{
    public class SpaceFormViewModel
    {
        public IEnumerable<SiteDto> SiteDtos { get; set; }

        public SpaceDto SpaceDto { get; set; }

    }
}