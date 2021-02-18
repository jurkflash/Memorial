using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Memorial.Core.Domain
{
    public class CremationTransaction
    {
        public CremationTransaction()
        {
            Invoices = new HashSet<Invoice>();

            Receipts = new HashSet<Receipt>();
        }

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

        public DateTime CreateDate { get; set; }

        public DateTime? ModifyDate { get; set; }

        public DateTime? DeleteDate { get; set; }

        public ICollection<Invoice> Invoices { get; set; }

        public ICollection<Receipt> Receipts { get; set; }
    }
}