using System.Collections.Generic;
using Memorial.Core.Dtos;

namespace Memorial.ViewModels
{
    public class FuneralCompanyFormViewModel
    {
        public IEnumerable<SiteDto> SiteDtos { get; set; }

        public FuneralCompanyDto FuneralCompanyDto { get; set; }

    }
}