using System.Collections.Generic;
using System.Linq;
using Memorial.Core;
using Memorial.Lib.Product;
using Memorial.Lib.SubProductService;

namespace Memorial.Lib.Urn
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

        public Core.Domain.UrnItem GetById(int id)
        {
            return _unitOfWork.UrnItems.GetActive(id);
        }

        public IEnumerable<Core.Domain.UrnItem> GetByUrn(int urnId)
        {
            return _unitOfWork.UrnItems.GetByUrn(urnId);
        }

        public bool IsOrder(Core.Domain.UrnItem urnItem)
        {
            if (urnItem.isOrder.HasValue)
                return urnItem.isOrder.Value;
            else
                return urnItem.SubProductService.isOrder;
        }

        public IEnumerable<Core.Domain.SubProductService> GetAvailableItemByUrn(int urnId)
        {
            if (urnId == 0)
                return new HashSet<Core.Domain.SubProductService>();

            var t = GetByUrn(urnId);
            var sp = _subProductService.GetByProduct(_product.GetUrnProduct().Id);
            var f = sp.Where(s => !t.Any(y => y.SubProductServiceId == s.Id));

            return (f);
        }

        public int Add(Core.Domain.UrnItem urnItem)
        {
            _unitOfWork.UrnItems.Add(urnItem);

            _unitOfWork.Complete();

            return urnItem.Id;
        }

        public bool Change(int id, Core.Domain.UrnItem urnItem)
        {
            var urnItemInDB = _unitOfWork.UrnItems.GetActive(id);

            if ((urnItemInDB.UrnId != urnItem.UrnId
                || urnItemInDB.isOrder != urnItem.isOrder)
                && _unitOfWork.UrnTransactions.Find(ct => ct.UrnItemId == urnItemInDB.Id).Any())
            {
                return false;
            }

            urnItemInDB.Price = urnItem.Price;
            urnItemInDB.Code = urnItem.Code;
            urnItemInDB.isOrder = urnItem.isOrder;
            _unitOfWork.Complete();

            return true;
        }

        public bool Remove(int id)
        {
            if (_unitOfWork.UrnTransactions.Find(ct => ct.UrnItemId == id).Any())
            {
                return false;
            }

            var urnItemInDB = _unitOfWork.UrnItems.GetActive(id);

            if (urnItemInDB == null)
            {
                return false;
            }

            _unitOfWork.UrnItems.Remove(urnItemInDB);

            _unitOfWork.Complete();

            return true;
        }
    }
}