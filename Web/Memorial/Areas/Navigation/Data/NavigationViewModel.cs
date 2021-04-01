using System.Collections.Generic;
using Memorial.Core.Dtos;

namespace Memorial.ViewModels
{
    public class NavigationViewModel
    {
        public IEnumerable<SiteDto> SiteDtos { get; set; }

        public int ApplicantId { get; set; }
    }
}