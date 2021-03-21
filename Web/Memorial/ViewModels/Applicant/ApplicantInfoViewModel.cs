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
        public int ApplicantId { get; set; }

        public IEnumerable<SiteDto> SiteDtos { get; set; }
    }
}