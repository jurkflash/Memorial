using System.Collections.Generic;
using Memorial.Core.Dtos;

namespace Memorial.ViewModels
{
    public class ColumbariumTransactionsFormViewModel
    {
        public IEnumerable<FuneralCompanyDto> FuneralCompanyDtos { get; set; }

        public IEnumerable<DeceasedBriefDto> DeceasedBriefDtos { get; set; }

        public ColumbariumTransactionDto ColumbariumTransactionDto { get; set; }

        public ColumbariumCentreDto ColumbariumCentreDto { get; set; }

        public int ShiftedNicheId { get; set; }

        public ApplicantDto ApplicantDto { get; set; }

        public NicheDto NicheDto { get; set; }
    }
}