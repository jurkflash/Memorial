using System.Collections.Generic;
using Memorial.Core.Dtos;

namespace Memorial.ViewModels
{
    public class ColumbariumAreaFormViewModel
    {
        public IEnumerable<SiteDto> SiteDtos { get; set; }

        public ColumbariumAreaDto ColumbariumAreaDto { get; set; }

    }
}