using System;
using System.ComponentModel.DataAnnotations;

namespace Memorial.Core.Dtos
{
    public class CremationTransactionDto
    {
        public string AF { get; set; }

        [StringLength(255)]
        public string Remark { get; set; }

        public float Price { get; set; }

        public CremationItemDto CremationItemDto { get; set; }

        public int CremationItemDtoId { get; set; }

        public DateTime CremateDate { get; set; }

        public FuneralCompanyDto FuneralCompanyDto { get; set; }

        public int? FuneralCompanyDtoId { get; set; }

        public ApplicantDto ApplicantDto { get; set; }

        public int ApplicantDtoId { get; set; }

        public DeceasedDto DeceasedDto { get; set; }

        public int DeceasedDtoId { get; set; }

        public string SummaryItem { get; set; }

        public DateTime CreatedUtcTime { get; set; }
    }
}