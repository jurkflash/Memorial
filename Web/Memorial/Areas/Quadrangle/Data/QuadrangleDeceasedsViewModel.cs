using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Memorial.Core.Domain;
using Memorial.Core.Dtos;

namespace Memorial.ViewModels
{
    public class QuadrangleDeceasedsViewModel
    {
        public ColumbariumTransactionDto QuadrangleTransactionDto { get; set; }

        public NicheDto QuadrangleDto { get; set; }

        public ApplicantDto ApplicantDto { get; set; }

        public int NumberOfPlacements { get; set; }

        public IEnumerable<DeceasedBriefDto> AvailableDeceaseds { get; set; }

        public int? Deceased1Id { get; set; }

        public int? Deceased2Id { get; set; }

        public ApplicantDeceasedFlattenDto DeceasedFlatten1Dto { get; set; }

        public ApplicantDeceasedFlattenDto DeceasedFlatten2Dto { get; set; }

    }
}