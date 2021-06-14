using System.Collections.Generic;
using Memorial.Core.Dtos;

namespace Memorial.ViewModels
{
    public class MiscellaneousFormViewModel
    {
        public IEnumerable<SiteDto> SiteDtos { get; set; }

        public MiscellaneousDto MiscellaneousDto { get; set; }

    }
}