using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Memorial.Core.Domain;
using Memorial.Core.Dtos;

namespace Memorial.ViewModels
{
    public class MiscellaneousItemIndexesViewModel
    {
        public IEnumerable<MiscellaneousTransactionDto> MiscellaneousTransactionDtos { get; set; }

        public int MiscellaneousItemId { get; set; }

        public int ApplicantId { get; set; }

        public bool OrderFlag { get; set; }
    }
}