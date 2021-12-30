using System;
using System.Collections.Generic;

namespace Memorial.Core.Domain
{
    public class CemeteryTransaction : Base
    {
        public CemeteryTransaction()
        {
            Invoices = new HashSet<Invoice>();

            Receipts = new HashSet<Receipt>();

            CemeteryTrackings = new HashSet<CemeteryTracking>();
        }

        public string AF { get; set; }

        public float Price { get; set; }

        public float? Maintenance { get; set; }

        public float? Wall { get; set; }

        public float? Dig { get; set; }

        public float? Brick { get; set; }

        public float Total { get; set; }

        public string Remark { get; set; }

        public CemeteryItem CemeteryItem { get; set; }

        public int CemeteryItemId { get; set; }

        public Plot Plot { get; set; }

        public int PlotId { get; set; }

        public FengShuiMaster FengShuiMaster { get; set; }

        public int? FengShuiMasterId { get; set; }

        public FuneralCompany FuneralCompany { get; set; }

        public int? FuneralCompanyId { get; set; }

        public Applicant Applicant { get; set; }

        public int ApplicantId { get; set; }

        public Deceased Deceased1 { get; set; }

        public int? Deceased1Id { get; set; }

        public Deceased Deceased2 { get; set; }

        public int? Deceased2Id { get; set; }

        public Deceased Deceased3 { get; set; }

        public int? Deceased3Id { get; set; }

        public Applicant ClearedApplicant { get; set; }

        public int? ClearedApplicantId { get; set; }

        public Applicant TransferredApplicant { get; set; }

        public int? TransferredApplicantId { get; set; }

        public CemeteryTransaction TransferredCemeteryTransaction { get; set; }

        public string TransferredCemeteryTransactionAF { get; set; }

        public string SummaryItem { get; set; }

        public DateTime? ClearanceDate { get; set; }

        public DateTime? ClearanceGroundDate { get; set; }

        public ICollection<Invoice> Invoices { get; set; }

        public ICollection<Receipt> Receipts { get; set; }

        public ICollection<CemeteryTracking> CemeteryTrackings { get; set; }
    }
}