﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Memorial.Core.Domain;
using Memorial.Core.Dtos;

namespace Memorial.ViewModels
{
    public class AncestorInfoViewModel
    {
        public SiteDto SiteDto { get; set; }

        public AncestralTabletAreaDto AncestralTabletAreaDto { get; set; }

        public AncestorDto AncestorDto { get; set; }

        public ApplicantDto ApplicantDto { get; set; }

        public ApplicantDeceasedFlattenDto DeceasedFlattenDto { get; set; }

    }
}