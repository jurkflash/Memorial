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

        public MiscellaneousItem MiscellaneousItem { get; set; }

        public int MiscellaneousItemId { get; set; }

        public Applicant Applicant { get; set; }

        public int ApplicantId { get; set; }

        public DateTime CreateDate { get; set; }
    }
}