using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Memorial.Core.Domain;

namespace Memorial.Core.Dtos
{
    public class SpaceTransactionDto
    {
        public string AF { get; set; }

        public DateTime? FromDate { get; set; }

        public DateTime? ToDate { get; set; }

        public float BasePrice { get; set; }

        public float Amount { get; set; }

        public string Remark { get; set; }

        public SpaceItem SpaceItem { get; set; }

        public int SpaceItemId { get; set; }

        public FuneralCompany FuneralCompany { get; set; }

        public int? FuneralCompanyId { get; set; }

        public Applicant Applicant { get; set; }

        public int ApplicantId { get; set; }

        public Deceased Deceased { get; set; }

        public int? DeceasedId { get; set; }

        public DateTime CreateDate { get; set; }
    }
}