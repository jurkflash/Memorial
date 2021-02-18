using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Memorial.Core.Domain
{
    public class PaymentMethod
    {
        public PaymentMethod()
        {
            Receipts = new HashSet<Receipt>();
        }

        public byte Id { get; set; }

        public string Name { get; set; }

        public Boolean RequireRemark { get; set; }

        public ICollection<Receipt> Receipts { get; set; }
    }
}