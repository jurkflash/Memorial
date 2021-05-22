using System;
using Memorial.Core.Domain;

namespace Memorial.Core.Dtos
{
    public class AncestralTabletTransactionDto
    {
        public AncestralTabletTransactionDto()
        {

        }

        public AncestralTabletTransactionDto(int ancestralTabletItemId, int ancestralTabletId, int applicantId)
        {
            AncestralTabletItemId = ancestralTabletItemId;
            AncestralTabletId = ancestralTabletId;
            ApplicantId = applicantId;
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

        public string ShiftedAncestralTabletTransactionAF { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime? ModifyDate { get; set; }

        public DateTime? DeleteDate { get; set; }
    }
}