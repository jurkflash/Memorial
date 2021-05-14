using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Memorial.Core.Domain;
using Memorial.Core.Dtos;

namespace Memorial.ViewModels
{
    public class PlotTransactionsFormViewModel
    {
        public IEnumerable<FuneralCompanyDto> FuneralCompanyDtos { get; set; }

        public IEnumerable<DeceasedBriefDto> DeceasedBriefDtos { get; set; }

        public IEnumerable<FengShuiMasterDto> FengShuiMasterDtos { get; set; }

        public PlotTransactionDto PlotTransactionDto { get; set; }

        public ApplicantDto ApplicantDto { get; set; }

        public PlotDto PlotDto { get; set; }
    }
}