using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Memorial.Core.Domain;
using Memorial.Core.Dtos;

namespace Memorial.ViewModels
{
    public class AncestralTabletItemsViewModel
    {
        public IEnumerable<AncestralTabletItemDto> AncestralTabletItemDtos { get; set; }

        public AncestralTabletDto AncestralTabletDto { get; set; }

        public int ApplicantId { get; set; }

        public SiteDto SiteDto { get; set; }

    }
}