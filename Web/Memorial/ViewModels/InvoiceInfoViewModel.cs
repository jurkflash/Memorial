using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Memorial.Core.Dtos;
using Memorial.Core.Domain;

namespace Memorial.ViewModels
{
    public class InvoiceInfoViewModel
    {
        public string AF { get; set; }

        public string SummaryItem { get; set; }

        public InvoiceDto InvoiceDto { get; set; }
    }
}