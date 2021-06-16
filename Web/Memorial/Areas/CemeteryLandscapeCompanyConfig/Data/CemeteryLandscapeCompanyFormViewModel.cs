using System.Collections.Generic;
using Memorial.Core.Dtos;

namespace Memorial.ViewModels
{
    public class CemeteryLandscapeCompanyFormViewModel
    {
        public IEnumerable<SiteDto> SiteDtos { get; set; }

        public CemeteryLandscapeCompanyDto CemeteryLandscapeCompanyDto { get; set; }

    }
}