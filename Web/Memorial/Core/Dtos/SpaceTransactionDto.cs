using System;
using System.ComponentModel.DataAnnotations;

namespace Memorial.Core.Dtos
{
    public class SpaceTransactionDto
    {
        public string AF { get; set; }

        public DateTime? FromDate { get; set; }

        public DateTime? ToDate { get; set; }

        public float BasePrice { get; set; }

        public float Amount { get; set; }

        public float OtherCharges { get; set; }

        [StringLength(255)]
        public string Remark { get; set; }

        public SpaceItemDto SpaceItemDto { get; set; }

        public int SpaceItemDtoId { get; set; }

        public FuneralCompanyDto FuneralCompanyDto { get; set; }

        public int? FuneralCompanyDtoId { get; set; }

        public ApplicantDto ApplicantDto { get; set; }

        public int ApplicantDtoId { get; set; }

        public DeceasedDto DeceasedDto { get; set; }

        public int? DeceasedDtoId { get; set; }

        public string SummaryItem { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}