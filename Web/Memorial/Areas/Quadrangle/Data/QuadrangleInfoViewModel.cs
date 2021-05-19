using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Memorial.Core.Domain;
using Memorial.Core.Dtos;

namespace Memorial.ViewModels
{
    public class QuadrangleInfoViewModel
    {
        public SiteDto SiteDto { get; set; }

        public ColumbariumCentreDto QuadrangleCentreDto { get; set; }

        public ColumbariumAreaDto QuadrangleAreaDto { get; set; }

        public NicheDto QuadrangleDto { get; set; }

        public ApplicantDto ApplicantDto { get; set; }

        public int NumberOfPlacements { get; set; }

        public ApplicantDeceasedFlattenDto DeceasedFlatten1Dto { get; set; }

        public ApplicantDeceasedFlattenDto DeceasedFlatten2Dto { get; set; }

    }
}