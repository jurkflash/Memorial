using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Memorial.Core.Dtos;
using Memorial.Core.Domain;

namespace Memorial.ViewModels
{
    public class ListInvoicesViewModel
    {
        public string AF { get; set; }
        public IEnumerable<InvoiceDto> InvoiceDtos { get; set; }
    }
}