using System.Collections.Generic;
using Memorial.Core.Dtos;

namespace Memorial.ViewModels
{
    public class CemeteryTransactionsFormViewModel
    {
        public IEnumerable<FuneralCompanyDto> FuneralCompanyDtos { get; set; }

        public IEnumerable<DeceasedBriefDto> DeceasedBriefDtos { get; set; }

        public IEnumerable<FengShuiMasterDto> FengShuiMasterDtos { get; set; }

        public CemeteryTransactionDto CemeteryTransactionDto { get; set; }

        public ApplicantDto ApplicantDto { get; set; }

        public PlotDto PlotDto { get; set; }
    }
}