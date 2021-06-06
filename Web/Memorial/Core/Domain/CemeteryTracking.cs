using System;

namespace Memorial.Core.Domain
{
    public class CemeteryTracking
    {
        public int Id { get; set; }

        public Plot Plot { get; set; }

        public int PlotId { get; set; }

        public CemeteryTransaction CemeteryTransaction { get; set; }

        public string CemeteryTransactionAF { get; set; }

        public Applicant Applicant { get; set; }

        public int? ApplicantId { get; set; }

        public Deceased Deceased1 { get; set; }

        public int? Deceased1Id { get; set; }

        public Deceased Deceased2 { get; set; }

        public int? Deceased2Id { get; set; }

        public Deceased Deceased3 { get; set; }

        public int? Deceased3Id { get; set; }

        public DateTime ActionDate { get; set; }

    }
}