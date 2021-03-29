using System;
using Memorial.Core.Domain;

namespace Memorial.Core.Dtos
{
    public class AncestorTransactionDto
    {
        public AncestorTransactionDto()
        {

        }

        public AncestorTransactionDto(int ancestorItemId, int ancestorId, int applicantId)
        {
            AncestorItemId = ancestorItemId;
            AncestorId = ancestorId;
            ApplicantId = applicantId;
        }

        public string AF { get; set; }

        public AncestorItem AncestorItem { get; set; }

        public int AncestorItemId { get; set; }

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

        public DateTime CreateDate { get; set; }

        public DateTime? ModifyDate { get; set; }

        public DateTime? DeleteDate { get; set; }
    }
}