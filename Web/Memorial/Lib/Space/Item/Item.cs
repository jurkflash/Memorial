using System.Collections.Generic;
using System.Linq;
using Memorial.Core;
using Memorial.Lib.Product;
using Memorial.Lib.SubProductService;
using AutoMapper;
using Memorial.Core.Domain;

namespace Memorial.Lib.Space
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

        public SpaceItem GetById(int id)
        {
            return _unitOfWork.SpaceItems.GetActive(id);
        }

        public float GetPrice(Core.Domain.SpaceItem spaceItem)
        {
            if (spaceItem.Price.HasValue)
                return spaceItem.Price.Value;
            else
                return spaceItem.SubProductService.Price;
        }

        public bool IsOrder(Core.Domain.SpaceItem spaceItem)
        {
            if (spaceItem.isOrder.HasValue)
                return spaceItem.isOrder.Value;
            else
                return spaceItem.SubProductService.isOrder;
        }

        public IEnumerable<Core.Domain.SpaceItem> GetBySpace(int spaceId)
        {
            return _unitOfWork.SpaceItems.GetBySpace(spaceId);
        }

        public IEnumerable<Core.Domain.SubProductService> GetAvailableItemBySpace(int spaceId)
        {
            if (spaceId == 0)
                return new HashSet<Core.Domain.SubProductService>();

            var t = GetBySpace(spaceId);
            var sp = _subProductService.GetByProduct(_product.GetSpaceProduct().Id);
            var f = sp.Where(s => !t.Any(y => y.SubProductServiceId == s.Id));

            return f;
        }

        public int Add(Core.Domain.SpaceItem spaceItem)
        {
            _unitOfWork.SpaceItems.Add(spaceItem);

            _unitOfWork.Complete();

            return spaceItem.Id;
        }

        public bool Change(int id, Core.Domain.SpaceItem spaceItem)
        {
            var spaceItemInDB = _unitOfWork.SpaceItems.GetActive(id);

            if ((spaceItemInDB.SpaceId != spaceItem.SpaceId
                || spaceItemInDB.isOrder != spaceItem.isOrder
                || spaceItemInDB.AllowDoubleBook != spaceItem.AllowDoubleBook
                || spaceItemInDB.AllowDeposit != spaceItem.AllowDeposit)
                && _unitOfWork.SpaceTransactions.Find(ct => ct.SpaceItemId == spaceItemInDB.Id).Any())
            {
                return false;
            }

            spaceItemInDB.Price = spaceItem.Price;
            spaceItemInDB.Code = spaceItem.Code;
            spaceItemInDB.isOrder = spaceItem.isOrder;

            _unitOfWork.Complete();

            return true;
        }

        public bool Remove(int id)
        {
            if (_unitOfWork.SpaceTransactions.Find(ct => ct.SpaceItemId == id).Any())
            {
                return false;
            }

            var item = _unitOfWork.SpaceItems.Get(id);

            if(item == null)
            {
                return false;
            }

            _unitOfWork.SpaceItems.Remove(item);

            _unitOfWork.Complete();

            return true;
        }

    }
}