using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Memorial.Core.Dtos;
using Memorial.Core.Domain;

namespace Memorial.ViewModels
{
    public class OrderReceiptsViewModel
    {
        public string AF { get; set; }

        public float RemainingAmount { get; set; } 

        public MasterCatalog MasterCatalog { get; set; }

        public InvoiceDto InvoiceDto { get; set; }

        public IEnumerable<ReceiptDto> ReceiptDtos { get; set; }
    }
}