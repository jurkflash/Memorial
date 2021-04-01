using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Memorial.Core.Domain;

namespace Memorial.ViewModels
{
    public class ListCatalogViewModel
    {
        public IEnumerable<Catalog> Catalogs { get; set; }
        public int ApplicantId { get; set; }
    }
}