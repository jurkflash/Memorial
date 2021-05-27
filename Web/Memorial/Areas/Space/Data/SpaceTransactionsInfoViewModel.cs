using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Memorial.Core.Domain;
using Memorial.Core.Dtos;

namespace Memorial.ViewModels
{
    public class SpaceTransactionsInfoViewModel
    {
        public SpaceTransactionDto SpaceTransactionDto { get; set; }

        public string ItemName { get; set; }

        public int TotalDays { get; set; }

        public float TotalAmount { get; set; }

        public SpaceDto SpaceDto { get; set; }

        public int ApplicantId { get; set; }

        public int? DeceasedId { get; set; }

        public bool ExportToPDF { get; set; }

        public string Header { get; set; }

    }
}