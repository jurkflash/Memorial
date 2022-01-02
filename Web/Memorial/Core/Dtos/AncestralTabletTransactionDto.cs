using System;
using System.ComponentModel.DataAnnotations;

namespace Memorial.Core.Dtos
{
    public class AncestralTabletTransactionDto
    {
        public AncestralTabletTransactionDto()
        {

        }

        public AncestralTabletTransactionDto(int ancestralTabletItemDtoId, int ancestralTabletDtoId, int applicantDtoId)
        {
            AncestralTabletItemDtoId = ancestralTabletItemDtoId;
            AncestralTabletDtoId = ancestralTabletDtoId;
            ApplicantDtoId = applicantDtoId;
        }

        public string AF { get; set; }

        public AncestralTabletItemDto AncestralTabletItemDto { get; set; }

        public int AncestralTabletItemDtoId { get; set; }

        public AncestralTabletDto AncestralTabletDto { get; set; }

        public int AncestralTabletDtoId { get; set; }

        public string Label { get; set; }

        [StringLength(255)]
        public string Remark { get; set; }

        public float Price { get; set; }

        public float? Maintenance { get; set; }

        public ApplicantDto ApplicantDto { get; set; }

        public int ApplicantDtoId { get; set; }

        public DeceasedDto DeceasedDto { get; set; }

        public int? DeceasedDtoId { get; set; }

        public DateTime? FromDate { get; set; }

        public DateTime? ToDate { get; set; }

        public AncestralTabletDto ShiftedAncestralTabletDto { get; set; }

        public int? ShiftedAncestralTabletDtoId { get; set; }

        public string ShiftedAncestralTabletTransactionDtoAF { get; set; }

        public string WithdrewAFS { get; set; }

        public int? WithdrewAncestralTabletApplicantId { get; set; }

        public string SummaryItem { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public DateTime? DeletedDate { get; set; }
    }
}