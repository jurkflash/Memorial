using System.Collections.Generic;
using Memorial.Core.Dtos;

namespace Memorial.ViewModels
{
    public class UrnFormViewModel
    {
        public IEnumerable<SiteDto> SiteDtos { get; set; }

        public UrnDto UrnDto { get; set; }

    }
}