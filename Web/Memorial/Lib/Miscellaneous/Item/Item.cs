using System.Collections.Generic;
using System.Linq;
using Memorial.Core;
using Memorial.Lib.Product;
using Memorial.Lib.SubProductService;

namespace Memorial.Lib.Miscellaneous
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

        public Core.Domain.MiscellaneousItem GetById(int id)
        {
            return _unitOfWork.MiscellaneousItems.GetActive(id);
        }

        public float GetPrice(Core.Domain.MiscellaneousItem miscellaneousItem)
        {
            if (miscellaneousItem.Price.HasValue)
                return miscellaneousItem.Price.Value;
            else
                return miscellaneousItem.SubProductService.Price;
        }

        public IEnumerable<Core.Domain.MiscellaneousItem> GetByMiscellaneous(int miscellaneousId)
        {
            return _unitOfWork.MiscellaneousItems.GetByMiscellaneous(miscellaneousId);
        }

        public IEnumerable<Core.Domain.SubProductService> GetAvailableItemByMiscellaneous(int miscellaneousId)
        {
            if (miscellaneousId == 0)
                return new HashSet<Core.Domain.SubProductService>();

            var t = GetByMiscellaneous(miscellaneousId);
            var sp = _subProductService.GetByProduct(_product.GetMiscellaneousProduct().Id);
            var f = sp.Where(s => !t.Any(y => y.SubProductServiceId == s.Id));

            return f;
        }

        public int Add(Core.Domain.MiscellaneousItem miscellaneousItem)
        {
            _unitOfWork.MiscellaneousItems.Add(miscellaneousItem);

            _unitOfWork.Complete();

            return miscellaneousItem.Id;
        }

        public bool Change(int id, Core.Domain.MiscellaneousItem miscellaneousItem)
        {
            var miscellaneousItemInDB = _unitOfWork.MiscellaneousItems.GetActive(id);

            if ((miscellaneousItemInDB.MiscellaneousId != miscellaneousItem.MiscellaneousId
                || miscellaneousItemInDB.isOrder != miscellaneousItem.isOrder)
                && _unitOfWork.MiscellaneousTransactions.Find(ct => ct.MiscellaneousItemId == miscellaneousItemInDB.Id).Any())
            {
                return false;
            }

            miscellaneousItemInDB.Price = miscellaneousItem.Price;
            miscellaneousItemInDB.Code = miscellaneousItem.Code;
            miscellaneousItemInDB.isOrder = miscellaneousItem.isOrder;
            _unitOfWork.Complete();

            return true;
        }

        public bool Remove(int id)
        {
            if (_unitOfWork.MiscellaneousTransactions.Find(ct => ct.MiscellaneousItemId == id).Any())
            {
                return false;
            }

            var item = _unitOfWork.MiscellaneousItems.Get(id);

            if (item == null)
            {
                return false;
            }

            _unitOfWork.MiscellaneousItems.Remove(item);

            _unitOfWork.Complete();

            return true;
        }
    }
}