using System;

namespace Memorial.Core.Domain
{
    public class ColumbariumTracking
    {
        public int Id { get; set; }

        public Quadrangle Quadrangle { get; set; }

        public int QuadrangleId { get; set; }

        public ColumbariumTransaction ColumbariumTransaction { get; set; }

        public string ColumbariumTransactionAF { get; set; }

        public Applicant Applicant { get; set; }

        public int? ApplicantId { get; set; }

        public Deceased Deceased1 { get; set; }

        public int? Deceased1Id { get; set; }

        public Deceased Deceased2 { get; set; }

        public int? Deceased2Id { get; set; }

        public DateTime ActionDate { get; set; }

    }
}