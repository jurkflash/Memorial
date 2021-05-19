using System.Collections.Generic;
using Memorial.Core.Dtos;

namespace Memorial.ViewModels
{
    public class NicheDeceasedsViewModel
    {
        public ColumbariumTransactionDto ColumbariumTransactionDto { get; set; }

        public NicheDto NicheDto { get; set; }

        public ApplicantDto ApplicantDto { get; set; }

        public int NumberOfPlacements { get; set; }

        public IEnumerable<DeceasedBriefDto> AvailableDeceaseds { get; set; }

        public int? Deceased1Id { get; set; }

        public int? Deceased2Id { get; set; }

        public ApplicantDeceasedFlattenDto DeceasedFlatten1Dto { get; set; }

        public ApplicantDeceasedFlattenDto DeceasedFlatten2Dto { get; set; }

    }
}