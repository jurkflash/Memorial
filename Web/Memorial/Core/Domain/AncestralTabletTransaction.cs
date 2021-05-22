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

            AncestorTrackings = new HashSet<AncestorTracking>();
        }

        public string AF { get; set; }

        public AncestralTabletItem AncestralTabletItem { get; set; }

        public int AncestralTabletItemId { get; set; }

        public Ancestor Ancestor { get; set; }

        public int AncestorId { get; set; }

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

        public Ancestor ShiftedAncestor { get; set; }

        public int? ShiftedAncestorId { get; set; }

        public AncestralTabletTransaction ShiftedAncestralTabletTransaction { get; set; }

        public string ShiftedAncestralTabletTransactionAF { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime? ModifyDate { get; set; }

        public DateTime? DeleteDate { get; set; }

        public ICollection<Invoice> Invoices { get; set; }

        public ICollection<Receipt> Receipts { get; set; }

        public ICollection<AncestorTracking> AncestorTrackings { get; set; }
    }
}