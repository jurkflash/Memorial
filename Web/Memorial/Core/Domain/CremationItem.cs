﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Memorial.Core.Domain
{
    public class CremationItem
    {
        public CremationItem()
        {
            CremationTransactions = new HashSet<CremationTransaction>();
        }

        public int Id { get; set; }

        public float? Price { get; set; }

        public string Code { get; set; }

        public Cremation Cremation { get; set; }

        public byte CremationId { get; set; }

        public bool? isOrder { get; set; }

        public SubProductService SubProductService { get; set; }

        public int SubProductServiceId { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime? ModifyDate { get; set; }

        public DateTime? DeleteDate { get; set; }

        public ICollection<CremationTransaction> CremationTransactions { get; set; }
    }
}