using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Memorial.Core.Domain
{
    public class AncestralTabletTransaction
    {
        public AncestralTabletTransaction()
        {
            Invoices = new HashSet<Invoice>();

            Receipts = new HashSet<Receipt>();

            AncestralTabletTrackings = new HashSet<AncestralTabletTracking>();
        }

        public string AF { get; set; }

        public AncestralTabletItem AncestralTabletItem { get; set; }

        public int AncestralTabletItemId { get; set; }

        public AncestralTablet AncestralTablet { get; set; }

        public int AncestralTabletId { get; set; }

        public string Label { get; set; }

        public string Remark { get; set; }

        public float Price { get; set; }

        public float? Maintenance { get; set; }

        public Applicant Applicant { get; set; }

        public int ApplicantId { get; set; }

        public Deceased Deceased { get; set; }

        public int? DeceasedId { get; set; }

        public DateTime? FromDate { get; set; }

        public DateTime? ToDate { get; set; }

        public AncestralTablet ShiftedAncestralTablet { get; set; }

        public int? ShiftedAncestralTabletId { get; set; }

        public AncestralTabletTransaction ShiftedAncestralTabletTransaction { get; set; }

        public string ShiftedAncestralTabletTransactionAF { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime? ModifyDate { get; set; }

        public DateTime? DeleteDate { get; set; }

        public ICollection<Invoice> Invoices { get; set; }

        public ICollection<Receipt> Receipts { get; set; }

        public ICollection<AncestralTabletTracking> AncestralTabletTrackings { get; set; }
    }
}