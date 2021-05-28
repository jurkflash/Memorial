using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Memorial.Core.Domain;

namespace Memorial.Core.Dtos
{
    public class MiscellaneousTransactionDto
    {
        public string AF { get; set; }

        public float BasePrice { get; set; }

        public float Amount { get; set; }

        public string Remark { get; set; }

        public MiscellaneousItemDto MiscellaneousItemDto { get; set; }

        public int MiscellaneousItemDtoId { get; set; }

        public ApplicantDto ApplicantDto { get; set; }

        public int? ApplicantDtoId { get; set; }

        public CemeteryLandscapeCompanyDto CemeteryLandscapeCompanyDto { get; set; }

        public int? CemeteryLandscapeCompanyDtoId { get; set; }

        public DateTime CreateDate { get; set; }
    }
}