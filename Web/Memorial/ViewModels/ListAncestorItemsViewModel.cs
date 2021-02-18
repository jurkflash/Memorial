using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Memorial.Core.Domain;

namespace Memorial.ViewModels
{
    public class ListAncestorItemsViewModel
    {
        public IEnumerable<AncestorItem> AncestorItems { get; set; }
        public AncestorArea AncestorArea { get; set; }
        public Ancestor Ancestor { get; set; }
    }
}