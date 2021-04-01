using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Memorial.Core.Domain;
using Memorial.Core.Dtos;

namespace Memorial.ViewModels
{
    public class UrnItemIndexesViewModel
    {
        public IEnumerable<UrnTransactionDto> UrnTransactionDtos { get; set; }

        public int UrnItemId { get; set; }

        public int ApplicantId { get; set; }

        public bool AllowNew { get; set; }
    }
}