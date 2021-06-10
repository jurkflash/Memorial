using System;
using System.Collections.Generic;
using System.Linq;
using Memorial.Core;
using Memorial.Core.Dtos;
using Memorial.Lib.Product;
using AutoMapper;

namespace Memorial.Lib.Catalog
{
    public class Catalog : ICatalog
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IProduct _product;

        private Core.Domain.Catalog _catalog;

        public Catalog(IUnitOfWork unitOfWork, IProduct product)
        {
            _unitOfWork = unitOfWork;
            _product = product;
        }

        public void SetCatalog(int id)
        {
            _catalog = _unitOfWork.Catalogs.Get(id);
        }

        public Core.Domain.Catalog GetCatalog()
        {
            return _catalog;
        }

        public CatalogDto GetCatalogDto()
        {
            return Mapper.Map<Core.Domain.Catalog, CatalogDto>(_catalog);
        }

        public Core.Domain.Catalog GetCatalog(int id)
        {
            return _unitOfWork.Catalogs.Get(id);
        }

        public CatalogDto GetCatalogDto(int id)
        {
            return Mapper.Map<Core.Domain.Catalog, CatalogDto>(GetCatalog(id));
        }

        public IEnumerable<Core.Domain.Catalog> GetCatalogs()
        {
            return _unitOfWork.Catalogs.GetAll();
        }

        public IEnumerable<CatalogDto> GetCatalogDtos()
        {
            return Mapper.Map<IEnumerable<Core.Domain.Catalog>, IEnumerable<CatalogDto>>(GetCatalogs());
        }

        public IEnumerable<Core.Domain.Catalog> GetCatalogsBySite(int id)
        {
            return _unitOfWork.Catalogs.GetBySite(id);
        }

        public IEnumerable<CatalogDto> GetCatalogDtosBySite(int id)
        {
            return Mapper.Map<IEnumerable<Core.Domain.Catalog>, IEnumerable<CatalogDto>>(GetCatalogsBySite(id));
        }

        public int CreateCatalog(CatalogDto catalogDto)
        {
            _catalog = new Core.Domain.Catalog();

            if (GetCatalogsBySite(catalogDto.SiteDtoId).Where(c => c.ProductId == catalogDto.ProductDtoId).Any())
                return 0;

            Mapper.Map(catalogDto, _catalog);

            _catalog.CreateDate = DateTime.Now;

            _unitOfWork.Catalogs.Add(_catalog);

            _unitOfWork.Complete();

            return _catalog.Id;
        }

        public bool DeleteCatalog(int id)
        {
            var catalog = GetCatalog(id);
            bool checkResult = false;

            if (catalog.Product.Area == _product.Cemetery)
                checkResult = _unitOfWork.CremationTransactions.Find(at => at.CremationItem.Cremation.SiteId == catalog.SiteId && at.DeleteDate == null).Any();

            if (catalog.Product.Area == _product.AncestralTablet)
                checkResult = _unitOfWork.AncestralTabletTransactions.Find(at => at.AncestralTabletItem.AncestralTabletArea.SiteId == catalog.SiteId && at.DeleteDate == null).Any();

            if (catalog.Product.Area == _product.Cremation)
                checkResult = _unitOfWork.CremationTransactions.Find(at => at.CremationItem.Cremation.SiteId == catalog.SiteId && at.DeleteDate == null).Any();

            if (catalog.Product.Area == _product.Urn)
                checkResult = _unitOfWork.UrnTransactions.Find(at => at.UrnItem.Urn.SiteId == catalog.SiteId && at.DeleteDate == null).Any();

            if (catalog.Product.Area == _product.Columbarium)
                checkResult = _unitOfWork.ColumbariumTransactions.Find(at => at.ColumbariumItem.ColumbariumCentre.SiteId == catalog.SiteId && at.DeleteDate == null).Any();

            if (catalog.Product.Area == _product.Space)
                checkResult = _unitOfWork.SpaceTransactions.Find(at => at.SpaceItem.Space.SiteId == catalog.SiteId && at.DeleteDate == null).Any();

            if (catalog.Product.Area == _product.Miscellaneous)
                checkResult = _unitOfWork.MiscellaneousTransactions.Find(at => at.MiscellaneousItem.Miscellaneous.SiteId == catalog.SiteId && at.DeleteDate == null).Any();

            if (!checkResult)
            {
                return false;
            }

            SetCatalog(id);

            _unitOfWork.Catalogs.Remove(_catalog);

            _unitOfWork.Complete();

            return true;
        }
    }
}