using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Memorial.Core.Dtos;
using Memorial.Core.Domain;

namespace Memorial.ViewModels
{
    public class CremationNonOrderReceiptsViewModel
    {
        public CremationTransactionDto CremationTransactionDto { get; set; }
        public IEnumerable<ReceiptDto> ReceiptDtos { get; set; }
    }
}