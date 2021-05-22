using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Memorial.Core.Domain;
using Memorial.Core.Dtos;
using PagedList;

namespace Memorial.ViewModels
{
    public class AncestralTabletItemIndexesViewModel
    {
        public IPagedList<AncestralTabletTransactionDto> AncestralTabletTransactionDtos { get; set; }

        public int AncestralTabletItemId { get; set; }

        public AncestralTabletDto AncestralTabletDto { get; set; }

        public int AncestralTabletId { get; set; }

        public int ApplicantId { get; set; }

        public bool OrderFlag { get; set; }

        public string SystemCode { get; set; }

        public bool AllowNew { get; set; }
    }
}