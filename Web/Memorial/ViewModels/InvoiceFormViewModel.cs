using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Memorial.Core.Dtos;
using Memorial.Core.Domain;

namespace Memorial.ViewModels
{
    public class InvoiceFormViewModel
    {
        public string AF { get; set; }

        public float Amount { get; set; }

        public InvoiceDto InvoiceDto { get; set; }
    }
}