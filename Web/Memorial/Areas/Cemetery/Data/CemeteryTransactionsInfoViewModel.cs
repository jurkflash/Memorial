using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Memorial.Core.Domain;
using Memorial.Core.Dtos;

namespace Memorial.ViewModels
{
    public class CemeteryTransactionsInfoViewModel
    {
        public CemeteryTransactionDto CemeteryTransactionDto { get; set; }

        public string ItemName { get; set; }

        public PlotDto PlotDto { get; set; }

        public ApplicantDto ApplicantDto { get; set; }

        public int ApplicantId { get; set; }

        public int? DeceasedId { get; set; }

        public bool ExportToPDF { get; set; }

        public string Header { get; set; }
    }
}