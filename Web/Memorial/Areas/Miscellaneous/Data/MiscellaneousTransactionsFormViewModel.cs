using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Memorial.Core.Domain;
using Memorial.Core.Dtos;

namespace Memorial.ViewModels
{
    public class MiscellaneousTransactionsFormViewModel
    {
        public IEnumerable<CemeteryLandscapeCompanyDto> CemeteryLandscapeCompanyDtos { get; set; }

        public MiscellaneousTransactionDto MiscellaneousTransactionDto { get; set; }

        public ApplicantDto ApplicantDto { get; set; }

        public MiscellaneousItemDto MiscellaneousItemDto { get; set; }
    }
}