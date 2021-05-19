using Memorial.Core.Dtos;

namespace Memorial.ViewModels
{
    public class ColumbariumInfoViewModel
    {
        public SiteDto SiteDto { get; set; }

        public ColumbariumCentreDto ColumbariumCentreDto { get; set; }

        public ColumbariumAreaDto ColumbariumAreaDto { get; set; }

        public NicheDto NicheDto { get; set; }

        public ApplicantDto ApplicantDto { get; set; }

        public int NumberOfPlacements { get; set; }

        public ApplicantDeceasedFlattenDto DeceasedFlatten1Dto { get; set; }

        public ApplicantDeceasedFlattenDto DeceasedFlatten2Dto { get; set; }

    }
}