using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Memorial.Core.Domain;
using Memorial.Core.Dtos;

namespace Memorial.ViewModels
{
    public class AncestralTabletTransactionsFormViewModel
    {
        public IEnumerable<DeceasedBriefDto> DeceasedBriefDtos { get; set; }

        public AncestralTabletTransactionDto AncestralTabletTransactionDto { get; set; }

        public ApplicantDto ApplicantDto { get; set; }

        public AncestralTabletDto AncestralTabletDto { get; set; }
    }
}