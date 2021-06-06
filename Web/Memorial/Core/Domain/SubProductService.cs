using System.Collections.Generic;

namespace Memorial.Core.Domain
{
    public class SubProductService
    {
        public SubProductService()
        {
            AncestralTabletItems = new HashSet<AncestralTabletItem>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public float Price { get; set; }

        public string Code { get; set; }

        public string SystemCode { get; set; }

        public bool isOrder { get; set; }

        public Product Product { get; set; }

        public int ProductId { get; set; }

        public ICollection<AncestralTabletItem> AncestralTabletItems { get; set; }
    }
}