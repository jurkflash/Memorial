using System.Collections.Generic;
using Memorial.Core.Dtos;

namespace Memorial.ViewModels
{
    public class CemeteryAreaFormViewModel
    {
        public IEnumerable<SiteDto> SiteDtos { get; set; }

        public CemeteryAreaDto CemeteryAreaDto { get; set; }

    }
}