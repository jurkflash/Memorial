using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Memorial.Core.Domain
{
    public class Product
    {
        public Product()
        {
            Catalogs = new HashSet<Catalog>();

            SubProductServices = new HashSet<SubProductService>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string Area { get; set; }

        public string Controller { get; set; }

        public ICollection<Catalog> Catalogs { get; set; }

        public ICollection<SubProductService> SubProductServices { get; set; }
    }
}