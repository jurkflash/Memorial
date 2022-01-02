using System;
using System.ComponentModel.DataAnnotations;

namespace Memorial.Core.Dtos
{
    public class ColumbariumTransactionDto
    {
        public ColumbariumTransactionDto()
        {

        }

        public ColumbariumTransactionDto(int columbariumItemId, int nicheId, int applicantId)
        {
            ColumbariumItemDtoId = columbariumItemId;
            NicheDtoId = nicheId;
            ApplicantDtoId = applicantId;
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

        [StringLength(255)]
        public string Remark { get; set; }

        public ColumbariumItemDto ColumbariumItemDto { get; set; }

        public int ColumbariumItemDtoId { get; set; }

        public NicheDto NicheDto { get; set; }

        public int NicheDtoId { get; set; }

        public FuneralCompanyDto FuneralCompanyDto { get; set; }

        public int? FuneralCompanyDtoId { get; set; }

        public ApplicantDto ApplicantDto { get; set; }

        public int ApplicantDtoId { get; set; }

        public DeceasedDto DeceasedDto1 { get; set; }

        public int? DeceasedDto1Id { get; set; }

        public DeceasedDto DeceasedDto2 { get; set; }

        public int? DeceasedDto2Id { get; set; }

        public NicheDto ShiftedNicheDto { get; set; }

        public int? ShiftedNicheDtoId { get; set; }

        public int? TransferredFromApplicantId { get; set; }

        public ColumbariumTransactionDto ShiftedColumbariumTransactionDto { get; set; }

        public string ShiftedColumbariumTransactionDtoAF { get; set; }

        public ApplicantDto TransferredApplicantDto { get; set; }

        public int? TransferredApplicantDtoId { get; set; }

        public ColumbariumTransactionDto TransferredColumbariumTransactionDto { get; set; }

        public string TransferredColumbariumTransactionDtoAF { get; set; }

        public string WithdrewAFS { get; set; }

        public int? WithdrewAncestralTabletApplicantId { get; set; }

        public string SummaryItem { get; set; }

        public DateTime CreatedDate { get; set; }

    }
}