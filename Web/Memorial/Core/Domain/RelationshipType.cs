using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Memorial.Core.Domain
{
    public class RelationshipType
    {
        public RelationshipType()
        {
            Deceaseds = new HashSet<Deceased>();

            AncestorTransactions = new HashSet<AncestorTransaction>();
        }

        public byte Id { get; set; }

        public string Name { get; set; }

        public ICollection<Deceased> Deceaseds { get; set; }

        public ICollection<AncestorTransaction> AncestorTransactions { get; set; }
    }
}