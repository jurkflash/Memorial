﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Memorial.Core.Domain
{
    public class Invoice : Base
    {
        public Invoice()
        {
            Receipts = new HashSet<Receipt>();
        }

        public string IV { get; set; }

        public float Amount { get; set; }

        public Boolean isPaid { get; set; }

        public Boolean hasReceipt { get; set; }

        public string Remark { get; set; }

        public CemeteryTransaction CemeteryTransaction { get; set; }

        public string CemeteryTransactionAF { get; set; }

        public CremationTransaction CremationTransaction { get; set; }

        public string CremationTransactionAF { get; set; }

        public AncestralTabletTransaction AncestralTabletTransaction { get; set; }

        public string AncestralTabletTransactionAF { get; set; }

        public MiscellaneousTransaction MiscellaneousTransaction { get; set; }

        public string MiscellaneousTransactionAF { get; set; }

        public ColumbariumTransaction ColumbariumTransaction { get; set; }

        public string ColumbariumTransactionAF { get; set; }

        public SpaceTransaction SpaceTransaction { get; set; }

        public string SpaceTransactionAF { get; set; }

        public UrnTransaction UrnTransaction { get; set; }

        public string UrnTransactionAF { get; set; }

        public bool AllowDeposit { get; set; }

        public ICollection<Receipt> Receipts { get; set; }
    }
}