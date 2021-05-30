using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Memorial.Core.Domain
{
    public class ColumbariumTransaction
    {
        public ColumbariumTransaction()
        {
            Invoices = new HashSet<Invoice>();

            Receipts = new HashSet<Receipt>();

            ColumbariumTrackings = new HashSet<ColumbariumTracking>();
        }

        public string AF { get; set; }

        public float Price { get; set; }

        public float? Maintenance { get; set; }

        public float? LifeTimeMaintenance { get; set; }

        public DateTime? FromDate { get; set; }

        public DateTime? ToDate { get; set; }

        public string Text1 { get; set; }

        public string Text2 { get; set; }

        public string Text3 { get; set; }

        public string Remark { get; set; }

        public ColumbariumItem ColumbariumItem { get; set; }

        public int ColumbariumItemId { get; set; }

        public Niche Niche { get; set; }

        public int NicheId { get; set; }

        public FuneralCompany FuneralCompany { get; set; }

        public int? FuneralCompanyId { get; set; }

        public Applicant Applicant { get; set; }

        public int ApplicantId { get; set; }

        public Deceased Deceased1 { get; set; }

        public int? Deceased1Id { get; set; }

        public Deceased Deceased2 { get; set; }

        public int? Deceased2Id { get; set; }

        public Niche ShiftedNiche { get; set; }

        public int? ShiftedNicheId { get; set; }

        public Applicant TransferredFromApplicant { get; set; }

        public int? TransferredFromApplicantId { get; set; }

        public ColumbariumTransaction ShiftedColumbariumTransaction { get; set; }

        public string ShiftedColumbariumTransactionAF { get; set; }

        public Applicant TransferredApplicant { get; set; }

        public int? TransferredApplicantId { get; set; }

        public ColumbariumTransaction TransferredColumbariumTransaction { get; set; }

        public string TransferredColumbariumTransactionAF { get; set; }

        public string WithdrewAFS { get; set; }

        public int? WithdrewColumbariumApplicantId { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime? ModifyDate { get; set; }

        public DateTime? DeleteDate { get; set; }

        public ICollection<Invoice> Invoices { get; set; }

        public ICollection<Receipt> Receipts { get; set; }

        public ICollection<ColumbariumTracking> ColumbariumTrackings { get; set; }
    }
}