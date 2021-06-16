using System.Collections.Generic;
using Memorial.Core.Dtos;

namespace Memorial.ViewModels
{
    public class FengShuiMasterFormViewModel
    {
        public IEnumerable<SiteDto> SiteDtos { get; set; }

        public FengShuiMasterDto FengShuiMasterDto { get; set; }

    }
}