using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Memorial.Core.Domain
{
    public class PlotTransaction
    {
        public PlotTransaction()
        {
            Invoices = new HashSet<Invoice>();

            Receipts = new HashSet<Receipt>();
        }

        public string AF { get; set; }

        public float Price { get; set; }

        public float Maintenance { get; set; }

        public float Wall { get; set; }

        public float Dig { get; set; }

        public float Brick { get; set; }

        public float Total { get; set; }

        public string Remark { get; set; }

        public PlotItem PlotItem { get; set; }

        public int PlotItemId { get; set; }

        public Plot Plot { get; set; }

        public int PlotId { get; set; }

        public Applicant Applicant { get; set; }

        public int ApplicantId { get; set; }

        public Deceased Deceased { get; set; }

        public int? DeceasedId { get; set; }

        public DateTime CreateDate { get; set; }

        public ICollection<Invoice> Invoices { get; set; }

        public ICollection<Receipt> Receipts { get; set; }
    }
}