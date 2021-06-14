using System.Collections.Generic;
using Memorial.Core.Dtos;

namespace Memorial.ViewModels
{
    public class CremationFormViewModel
    {
        public IEnumerable<SiteDto> SiteDtos { get; set; }

        public CremationDto CremationDto { get; set; }

    }
}