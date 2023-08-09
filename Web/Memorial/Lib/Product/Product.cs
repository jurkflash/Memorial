using System.Collections.Generic;
using System.Linq;
using Memorial.Core;

namespace Memorial.Lib.Product
{
    public class Product : IProduct
    {
        private readonly string MyCemetery = "Cemetery";
        private readonly string MyCemeteries = "Cemeteries";
        private readonly string MyAncestralTablet = "AncestralTablet";
        private readonly string MyAncestralTablets = "AncestralTablets";
        private readonly string MyCremation = "Cremation";
        private readonly string MyCremations = "Cremations";
        private readonly string MyUrn = "Urn";
        private readonly string MyUrns = "Urns";
        private readonly string MyColumbarium = "Columbarium";
        private readonly string MyColumbariums = "Columbariums";
        private readonly string MySpace = "Space";
        private readonly string MySpaces = "Spaces";
        private readonly string MyMiscellaneous = "Miscellaneous";

        public string Cemetery { get { return MyCemetery; } }
        public string Cemeteries { get { return MyCemeteries; } }
        public string AncestralTablet { get { return MyAncestralTablet; } }
        public string AncestralTablets { get { return MyAncestralTablets; } }
        public string Cremation { get { return MyCremation; } }
        public string Cremations { get { return MyCremations; } }
        public string Urn { get { return MyUrn; } }
        public string Urns { get { return MyUrns; } }
        public string Columbarium { get { return MyColumbarium; } }
        public string Columbariums { get { return MyColumbariums; } }
        public string Space { get { return MySpace; } }
        public string Spaces { get { return MySpaces; } }
        public string Miscellaneous { get { return MyMiscellaneous; } }

        private readonly IUnitOfWork _unitOfWork;

        public Product(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Core.Domain.Product Get(int id)
        {
            return _unitOfWork.Products.Get(id);
        }

        public IEnumerable<Core.Domain.Product> GetAll()
        {
            return _unitOfWork.Products.GetAll();
        }

        public Core.Domain.Product GetAncestralTabletProduct()
        {
            return _unitOfWork.Products.Find(p => p.Area == MyAncestralTablet).FirstOrDefault();
        }

        public Core.Domain.Product GetCemeteryProduct()
        {
            return _unitOfWork.Products.Find(p => p.Area == MyCemetery).FirstOrDefault();
        }

        public Core.Domain.Product GetCremationProduct()
        {
            return _unitOfWork.Products.Find(p => p.Area == MyCremation).FirstOrDefault();
        }

        public Core.Domain.Product GetUrnProduct()
        {
            return _unitOfWork.Products.Find(p => p.Area == MyUrn).FirstOrDefault();
        }

        public Core.Domain.Product GetColumbariumProduct()
        {
            return _unitOfWork.Products.Find(p => p.Area == MyColumbarium).FirstOrDefault();
        }

        public Core.Domain.Product GetSpaceProduct()
        {
            return _unitOfWork.Products.Find(p => p.Area == MySpace).FirstOrDefault();
        }

        public Core.Domain.Product GetMiscellaneousProduct()
        {
            return _unitOfWork.Products.Find(p => p.Area == MyMiscellaneous).FirstOrDefault();
        }
    }
}