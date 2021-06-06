using System.Collections.Generic;

namespace Memorial.Core.Domain
{
    public class SubProductService
    {
        public SubProductService()
        {
            AncestralTabletItems = new HashSet<AncestralTabletItem>();

            ColumbariumItems = new HashSet<ColumbariumItem>();

            CremationItems = new HashSet<CremationItem>();

            MiscellaneousItems = new HashSet<MiscellaneousItem>();
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

        public ICollection<ColumbariumItem> ColumbariumItems { get; set; }

        public ICollection<CremationItem> CremationItems { get; set; }

        public ICollection<MiscellaneousItem> MiscellaneousItems { get; set; }
    }
}