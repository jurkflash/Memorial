using System;

namespace Memorial.Core.Domain
{
    public class ColumbariumTracking
    {
        public int Id { get; set; }

        public Niche Niche { get; set; }

        public int NicheId { get; set; }

        public ColumbariumTransaction ColumbariumTransaction { get; set; }

        public string ColumbariumTransactionAF { get; set; }

        public Applicant Applicant { get; set; }

        public int? ApplicantId { get; set; }

        public Deceased Deceased1 { get; set; }

        public int? Deceased1Id { get; set; }

        public Deceased Deceased2 { get; set; }

        public int? Deceased2Id { get; set; }

        public bool ToDeleteFlag { get; set; }

        public DateTime ActionDate { get; set; }

    }
}