using Memorial.Core.Dtos;

namespace Memorial.ViewModels
{
    public class PlotInfoViewModel
    {
        public SiteDto SiteDto { get; set; }

        public PlotAreaDto PlotAreaDto { get; set; }

        public PlotDto PlotDto { get; set; }

        public ApplicantDto ApplicantDto { get; set; }

        public int NumberOfPlacements { get; set; }

        public ApplicantDeceasedFlattenDto DeceasedFlatten1Dto { get; set; }

        public ApplicantDeceasedFlattenDto DeceasedFlatten2Dto { get; set; }

        public ApplicantDeceasedFlattenDto DeceasedFlatten3Dto { get; set; }

    }
}