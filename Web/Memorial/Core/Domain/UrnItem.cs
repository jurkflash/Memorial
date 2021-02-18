using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Memorial.Core.Domain
{
    public class UrnItem
    {
        public UrnItem()
        {
            UrnNumbers = new HashSet<UrnNumber>();

            UrnTransactions = new HashSet<UrnTransaction>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public float Price { get; set; }

        public string Code { get; set; }

        public Urn Urn { get; set; }

        public int UrnId { get; set; }

        public string SystemCode { get; set; }

        public bool isOrder { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime? ModifyDate { get; set; }

        public DateTime? DeleteDate { get; set; }

        public ICollection<UrnNumber> UrnNumbers { get; set; }

        public ICollection<UrnTransaction> UrnTransactions { get; set; }
    }
}