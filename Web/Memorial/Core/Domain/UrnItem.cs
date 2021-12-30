using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Memorial.Core.Domain
{
    public class UrnItem : Base
    {
        public UrnItem()
        {
            UrnTransactions = new HashSet<UrnTransaction>();
        }

        public int Id { get; set; }

        public float? Price { get; set; }

        public string Code { get; set; }

        public Urn Urn { get; set; }

        public int UrnId { get; set; }

        public bool? isOrder { get; set; }

        public SubProductService SubProductService { get; set; }

        public int SubProductServiceId { get; set; }

        public ICollection<UrnTransaction> UrnTransactions { get; set; }
    }
}