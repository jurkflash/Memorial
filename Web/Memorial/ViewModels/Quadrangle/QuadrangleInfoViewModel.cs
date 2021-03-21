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

        public QuadrangleCentreDto QuadrangleCentreDto { get; set; }

        public QuadrangleAreaDto QuadrangleAreaDto { get; set; }

        public QuadrangleDto QuadrangleDto { get; set; }

        public ApplicantDto ApplicantDto { get; set; }

        public ApplicantDeceasedFlattenDto DeceasedFlatten1Dto { get; set; }

        public ApplicantDeceasedFlattenDto DeceasedFlatten2Dto { get; set; }

    }
}