using System.Collections.Generic;
using Memorial.Core.Dtos;

namespace Memorial.ViewModels
{
    public class AppplicantDeceasedsIndexViewModel
    {
        public ApplicantDto ApplicantDto { get; set; }

        public IEnumerable<DeceasedDto> DeceasedDtos { get; set; }

        public IEnumerable<ApplicantDeceasedDto> ApplicantDeceasedDtos { get; set; }
    }
}