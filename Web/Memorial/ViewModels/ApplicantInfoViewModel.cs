using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Memorial.Core.Dtos;
using Memorial.Core.Domain;

namespace Memorial.ViewModels
{
    public class ApplicantInfoViewModel
    {
        public ApplicantDto ApplicantDto { get; set; }
        public IEnumerable<DeceasedDto> DeceasedDtos { get; set; }
        public IEnumerable<Site> Sites { get; set; }
    }
}