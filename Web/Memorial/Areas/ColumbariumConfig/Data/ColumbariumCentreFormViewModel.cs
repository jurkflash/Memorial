using System.Collections.Generic;
using Memorial.Core.Dtos;

namespace Memorial.ViewModels
{
    public class ColumbariumCentreFormViewModel
    {
        public IEnumerable<SiteDto> SiteDtos { get; set; }

        public ColumbariumCentreDto ColumbariumCentreDto { get; set; }

    }
}