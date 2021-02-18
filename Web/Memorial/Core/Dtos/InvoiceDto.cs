﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Memorial.Core.Domain;

namespace Memorial.Core.Dtos
{
    public class InvoiceDto
    {
        public string IV { get; set; }

        public float Amount { get; set; }

        public Boolean isPaid { get; set; }

        public Boolean hasReceipt { get; set; }

        public string Remark { get; set; }

        public DateTime CreateDate { get; set; }

        public PlotTransaction PlotTransaction { get; set; }

        public string PlotTransactionAF { get; set; }

        public CremationTransaction CremationTransaction { get; set; }

        public string CremationTransactionAF { get; set; }

        public AncestorTransaction AncestorTransaction { get; set; }

        public string AncestorTransactionAF { get; set; }

        public MiscellaneousTransaction MiscellaneousTransaction { get; set; }

        public string MiscellaneousTransactionAF { get; set; }

        public QuadrangleTransaction QuadrangleTransaction { get; set; }

        public string QuadrangleTransactionAF { get; set; }

        public SpaceTransaction SpaceTransaction { get; set; }

        public string SpaceTransactionAF { get; set; }

        public UrnTransaction UrnTransaction { get; set; }

        public string UrnTransactionAF { get; set; }
    }
}