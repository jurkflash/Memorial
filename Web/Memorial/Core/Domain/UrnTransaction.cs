using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Memorial.Core.Domain
{
    public class UrnTransaction
    {
        public UrnTransaction()
        {
            Invoices = new HashSet<Invoice>();

            Receipts = new HashSet<Receipt>();
        }

        public string AF { get; set; }

        public float Price { get; set; }

        public string Remark { get; set; }

        public UrnItem UrnItem { get; set; }

        public int UrnItemId { get; set; }

        public Applicant Applicant { get; set; }

        public int ApplicantId { get; set; }

        public ICollection<Invoice> Invoices { get; set; }

        public ICollection<Receipt> Receipts { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime? ModifyDate { get; set; }

        public DateTime? DeleteDate { get; set; }
    }
}