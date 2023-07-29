using System.Collections.Generic;
using Memorial.Core.Dtos;

namespace Memorial.ViewModels
{
    public class ApplicantFormViewModel
    {
        public IEnumerable<SiteDto> SiteDtos { get; set; }

        public ApplicantDto ApplicantDto { get; set; }
    }
}