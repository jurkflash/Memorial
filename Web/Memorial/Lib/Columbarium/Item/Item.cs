using System;
using System.Collections.Generic;
using System.Linq;
using Memorial.Core;
using Memorial.Lib.Product;
using Memorial.Lib.SubProductService;

namespace Memorial.Lib.Columbarium
{
    public class Item : IItem
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IProduct _product;
        private readonly ISubProductService _subProductService;

        public Item(IUnitOfWork unitOfWork, IProduct product, ISubProductService subProductService)
        {
            _unitOfWork = unitOfWork;
            _product = product;
            _subProductService = subProductService;
        }

        public Core.Domain.ColumbariumItem GetById(int id)
        {
            return _unitOfWork.ColumbariumItems.GetActive(id);
        }

        public float GetPrice(Core.Domain.ColumbariumItem columbariumItem)
        {
            if (columbariumItem.Price.HasValue)
                return columbariumItem.Price.Value;
            else
                return columbariumItem.SubProductService.Price;
        }

        public bool IsOrder(Core.Domain.ColumbariumItem columbariumItem)
        {
            if (columbariumItem.isOrder.HasValue)
                return columbariumItem.isOrder.Value;
            else
                return columbariumItem.SubProductService.isOrder;
        }

        public float GetAmountWithDateRange(int itemId, DateTime from, DateTime to)
        {
            var item = _unitOfWork.ColumbariumItems.GetActive(itemId);

            if (from > to)
                return -1;

            var total = (((to.Year - from.Year) * 12) + to.Month - from.Month) * GetPrice(item);

            return total;
        }

        public IEnumerable<Core.Domain.ColumbariumItem> GetByCentre(int centreId)
        {
            return _unitOfWork.ColumbariumItems.GetByCentre(centreId);
        }

        public IEnumerable<Core.Domain.SubProductService> GetAvailableItemByCentre(int centreId)
        {
            if (centreId == 0)
                return new HashSet<Core.Domain.SubProductService>();

            var t = GetByCentre(centreId);
            var sp = _subProductService.GetByProduct(_product.GetColumbariumProduct().Id);
            var f = sp.Where(s => !t.Any(y => y.SubProductServiceId == s.Id));

            return f;
        }

        public int Add(Core.Domain.ColumbariumItem columbariumItem)
        {
            _unitOfWork.ColumbariumItems.Add(columbariumItem);

            _unitOfWork.Complete();

            return columbariumItem.Id;
        }

        public bool Change(int id, Core.Domain.ColumbariumItem columbariumItem)
        {
            var columbariumItemInDB = _unitOfWork.ColumbariumItems.GetActive(id);

            if ((columbariumItemInDB.isOrder != columbariumItem.isOrder
                || columbariumItemInDB.ColumbariumCentreId != columbariumItem.ColumbariumCentreId)
                && _unitOfWork.ColumbariumTransactions.Find(qi => qi.ColumbariumItemId == columbariumItem.Id).Any())
            {
                return false;
            }

            columbariumItemInDB.Price = columbariumItem.Price;
            columbariumItemInDB.Code = columbariumItem.Code;
            columbariumItemInDB.isOrder = columbariumItem.isOrder;
            _unitOfWork.Complete();

            return true;
        }

        public bool Remove(int id)
        {
            if (_unitOfWork.ColumbariumTransactions.Find(qt => qt.ColumbariumItemId == id).Any())
            {
                return false;
            }

            var item = _unitOfWork.ColumbariumItems.GetActive(id);

            if (item == null)
            {
                return false;
            }

            _unitOfWork.ColumbariumItems.Remove(item);

            _unitOfWork.Complete();

            return true;
        }

    }
}