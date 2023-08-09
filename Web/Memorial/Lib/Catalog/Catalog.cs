using System.Collections.Generic;
using System.Linq;
using Memorial.Core;
using Memorial.Lib.Product;
using AutoMapper;

namespace Memorial.Lib.Catalog
{
    public class Catalog : ICatalog
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IProduct _product;

        public Catalog(IUnitOfWork unitOfWork, IProduct product)
        {
            _unitOfWork = unitOfWork;
            _product = product;
        }

        public Core.Domain.Catalog Get(int id)
        {
            return _unitOfWork.Catalogs.Get(id);
        }

        public IEnumerable<Core.Domain.Catalog> GetAll()
        {
            return _unitOfWork.Catalogs.GetAllActive();
        }

        public IEnumerable<Core.Domain.Catalog> GetBySite(int siteId)
        {
            return _unitOfWork.Catalogs.GetBySite(siteId);
        }

        public IEnumerable<Core.Domain.Product> GetAvailableBySite(int siteId)
        {
            if (siteId == 0)
                return new HashSet<Core.Domain.Product>();

            var t = _unitOfWork.Catalogs.GetBySite(siteId);
            var p = Mapper.Map<IEnumerable<Core.Domain.Product>>(_product.GetAll());
            var f = p.Where(s => !t.Any(y => y.ProductId == s.Id));

            return f;
        }

        public IEnumerable<Core.Domain.Site> GetSitesAncestralTablet()
        {
            return _unitOfWork.Catalogs.GetByProduct(_product.GetAncestralTabletProduct().Id);
        }

        public IEnumerable<Core.Domain.Site> GetSitesCemetery()
        {
            return _unitOfWork.Catalogs.GetByProduct(_product.GetCemeteryProduct().Id);
        }

        public IEnumerable<Core.Domain.Site> GetSitesCremation()
        {
            return _unitOfWork.Catalogs.GetByProduct(_product.GetCremationProduct().Id);
        }

        public IEnumerable<Core.Domain.Site> GetSitesUrn()
        {
            return _unitOfWork.Catalogs.GetByProduct(_product.GetUrnProduct().Id);
        }

        public IEnumerable<Core.Domain.Site> GetSitesColumbarium()
        {
            return _unitOfWork.Catalogs.GetByProduct(_product.GetColumbariumProduct().Id);
        }

        public IEnumerable<Core.Domain.Site> GetSitesSpace()
        {
            return _unitOfWork.Catalogs.GetByProduct(_product.GetSpaceProduct().Id);
        }

        public IEnumerable<Core.Domain.Site> GetSitesMiscellaneous()
        {
            return _unitOfWork.Catalogs.GetByProduct(_product.GetMiscellaneousProduct().Id);
        }

        public int Add(Core.Domain.Catalog catalog)
        {
            if (_unitOfWork.Catalogs.GetBySite(catalog.SiteId).Where(c => c.ProductId == catalog.ProductId).Any())
                return 0;

            _unitOfWork.Catalogs.Add(catalog);

            _unitOfWork.Complete();

            return catalog.Id;
        }

        public bool Remove(int id)
        {
            var catalog = _unitOfWork.Catalogs.Get(id);
            if(catalog == null) 
                return false;

            bool checkResult = false;

            if (catalog.Product.Area == _product.Cemetery)
                checkResult = _unitOfWork.CremationTransactions.Find(at => at.CremationItem.Cremation.SiteId == catalog.SiteId).Any();

            if (catalog.Product.Area == _product.AncestralTablet)
                checkResult = _unitOfWork.AncestralTabletTransactions.Find(at => at.AncestralTabletItem.AncestralTabletArea.SiteId == catalog.SiteId).Any();

            if (catalog.Product.Area == _product.Cremation)
                checkResult = _unitOfWork.CremationTransactions.Find(at => at.CremationItem.Cremation.SiteId == catalog.SiteId).Any();

            if (catalog.Product.Area == _product.Urn)
                checkResult = _unitOfWork.UrnTransactions.Find(at => at.UrnItem.Urn.SiteId == catalog.SiteId).Any();

            if (catalog.Product.Area == _product.Columbarium)
                checkResult = _unitOfWork.ColumbariumTransactions.Find(at => at.ColumbariumItem.ColumbariumCentre.SiteId == catalog.SiteId).Any();

            if (catalog.Product.Area == _product.Space)
                checkResult = _unitOfWork.SpaceTransactions.Find(at => at.SpaceItem.Space.SiteId == catalog.SiteId).Any();

            if (catalog.Product.Area == _product.Miscellaneous)
                checkResult = _unitOfWork.MiscellaneousTransactions.Find(at => at.MiscellaneousItem.Miscellaneous.SiteId == catalog.SiteId).Any();

            if (checkResult)
            {
                return false;
            }

            _unitOfWork.Catalogs.Remove(catalog);

            _unitOfWork.Complete();

            return true;
        }
    }
}