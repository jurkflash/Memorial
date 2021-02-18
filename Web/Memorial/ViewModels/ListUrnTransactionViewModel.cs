using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Memorial.Core.Domain;

namespace Memorial.ViewModels
{
    public class ListUrnTransactionViewModel
    {
        public IEnumerable<UrnTransaction> UrnTransactions { get; set; }
        public UrnItem UrnItem { get; set; }
    }
}