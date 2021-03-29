using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Memorial.Core.Domain;
using Memorial.Core.Dtos;

namespace Memorial.ViewModels
{
    public class SpaceTransactionsFormViewModel
    {
        public IEnumerable<FuneralCompanyDto> FuneralCompanyDtos { get; set; }

        public IEnumerable<DeceasedBriefDto> DeceasedBriefDtos { get; set; }

        public SpaceTransactionDto SpaceTransactionDto { get; set; }

    }
}