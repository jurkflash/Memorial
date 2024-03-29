﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Memorial.Core.Domain;
using Memorial.Core.Dtos;

namespace Memorial.ViewModels
{
    public class AncestralTabletAreaFormViewModel
    {
        public IEnumerable<SiteDto> SiteDtos { get; set; }

        public AncestralTabletAreaDto AncestralTabletAreaDto { get; set; }

    }
}