using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Memorial.Core.Domain
{
    public class MiscellaneousTransaction : Base
    {
        public MiscellaneousTransaction()
        {
            Invoices = new HashSet<Invoice>();

            Receipts = new HashSet<Receipt>();
        }

        public string AF { get; set; }

        public float BasePrice { get; set; }

        public float Amount { get; set; }

        public string Remark { get; set; }

        public MiscellaneousItem MiscellaneousItem { get; set; }

        public int MiscellaneousItemId { get; set; }

        public Applicant Applicant { get; set; }

        public int? ApplicantId { get; set; }

        public CemeteryLandscapeCompany CemeteryLandscapeCompany { get; set; }

        public int? CemeteryLandscapeCompanyId { get; set; }

        public string SummaryItem { get; set; }

        public ICollection<Invoice> Invoices { get; set; }

        public ICollection<Receipt> Receipts { get; set; }
    }
}