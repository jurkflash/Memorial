using System.Collections.Generic;
using Memorial.Core;
using System.Linq;
using Memorial.Lib.Product;
using Memorial.Lib.SubProductService;
using AutoMapper;

namespace Memorial.Lib.AncestralTablet
{
    public class Item : IItem
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISubProductService _subProductService;
        private readonly IProduct _product;

        public Item(IUnitOfWork unitOfWork, IProduct product, ISubProductService subProductService)
        {
            _unitOfWork = unitOfWork;
            _product = product;
            _subProductService = subProductService;
        }

        public Core.Domain.AncestralTabletItem GetById(int id)
        {
            return _unitOfWork.AncestralTabletItems.GetActive(id);
        }

        public float GetPrice(Core.Domain.AncestralTabletItem ancestralTabletItem)
        {
            if (ancestralTabletItem.Price.HasValue)
                return ancestralTabletItem.Price.Value;
            else
                return ancestralTabletItem.SubProductService.Price;
        }

        public bool IsOrder(Core.Domain.AncestralTabletItem ancestralTabletItem)
        {
            if (ancestralTabletItem.isOrder.HasValue)
                return ancestralTabletItem.isOrder.Value;
            else
                return ancestralTabletItem.SubProductService.isOrder;
        }

        public IEnumerable<Core.Domain.AncestralTabletItem> GetByArea(int areaId)
        {
            return _unitOfWork.AncestralTabletItems.GetByArea(areaId);
        }

        public IEnumerable<Core.Domain.SubProductService> GetAvailableItemByArea(int areaId)
        {
            if (areaId == 0)
                return new HashSet<Core.Domain.SubProductService>();

            var t = GetByArea(areaId);
            var sp = _subProductService.GetByProduct(_product.GetAncestralTabletProduct().Id);
            var f = sp.Where(s => !t.Any(y => y.SubProductServiceId == s.Id));

            return f;
        }

        public int Add(Core.Domain.AncestralTabletItem ancestralTabletItem)
        {
            _unitOfWork.AncestralTabletItems.Add(ancestralTabletItem);

            _unitOfWork.Complete();

            return ancestralTabletItem.Id;
        }

        public bool Change(int id, Core.Domain.AncestralTabletItem ancestralTabletItem)
        {
            var ancestralTabletItemInDB = _unitOfWork.AncestralTabletItems.GetActive(id);

            if ((ancestralTabletItemInDB.AncestralTabletAreaId != ancestralTabletItem.AncestralTabletAreaId
                || ancestralTabletItemInDB.isOrder != ancestralTabletItem.isOrder)
                && _unitOfWork.AncestralTabletTransactions.Find(at => at.AncestralTabletItemId == ancestralTabletItemInDB.Id).Any())
            {
                return false;
            }

            ancestralTabletItemInDB.Price = ancestralTabletItem.Price;
            ancestralTabletItemInDB.Code = ancestralTabletItem.Code;
            ancestralTabletItemInDB.isOrder = ancestralTabletItem.isOrder;
            _unitOfWork.Complete();

            return true;
        }

        public bool Remove(int id)
        {
            if (_unitOfWork.AncestralTabletTransactions.Find(at => at.AncestralTabletItemId == id).Any())
            {
                return false;
            }

            var ancestralTabletItemInDB = _unitOfWork.AncestralTabletItems.GetActive(id);
            if (ancestralTabletItemInDB == null)
            {
                return false;
            }

            _unitOfWork.AncestralTabletItems.Remove(ancestralTabletItemInDB);
            
            _unitOfWork.Complete();

            return true;
        }
    }
}