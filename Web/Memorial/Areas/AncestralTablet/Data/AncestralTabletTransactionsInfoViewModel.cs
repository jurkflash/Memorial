using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Memorial.Core.Domain;
using Memorial.Core.Dtos;

namespace Memorial.ViewModels
{
    public class AncestralTabletTransactionsInfoViewModel
    {
        public AncestralTabletTransactionDto AncestralTabletTransactionDto { get; set; }

        public string ItemName { get; set; }

        public AncestralTabletDto AncestralTabletDto { get; set; }

        public int ApplicantId { get; set; }

        public int? DeceasedId { get; set; }
    }
}