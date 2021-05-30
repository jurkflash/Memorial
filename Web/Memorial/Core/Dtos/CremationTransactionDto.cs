using System;
using Memorial.Core.Domain;

namespace Memorial.Core.Dtos
{
    public class CremationTransactionDto
    {
        public string AF { get; set; }

        public string Remark { get; set; }

        public float Price { get; set; }

        public CremationItem CremationItem { get; set; }

        public int CremationItemId { get; set; }

        public DateTime CremateDate { get; set; }

        public FuneralCompany FuneralCompany { get; set; }

        public int? FuneralCompanyId { get; set; }

        public Applicant Applicant { get; set; }

        public int ApplicantId { get; set; }

        public Deceased Deceased { get; set; }

        public int DeceasedId { get; set; }

        public string SummaryItem { get; set; }

        public DateTime CreateDate { get; set; }
    }
}