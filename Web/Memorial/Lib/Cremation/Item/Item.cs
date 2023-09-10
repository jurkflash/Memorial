using System.Collections.Generic;
using System.Linq;
using Memorial.Core;
using Memorial.Lib.Product;
using Memorial.Lib.SubProductService;
using AutoMapper;
using Memorial.Core.Domain;

namespace Memorial.Lib.Cremation
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

        public Core.Domain.CremationItem GetById(int id)
        {
            return _unitOfWork.CremationItems.GetActive(id);
        }

        public float GetPrice(Core.Domain.CremationItem cremationItem)
        {
            if (cremationItem.Price.HasValue)
                return cremationItem.Price.Value;
            else
                return cremationItem.SubProductService.Price;
        }

        public IEnumerable<Core.Domain.CremationItem> GetByCremation(int cremationId)
        {
            return _unitOfWork.CremationItems.GetByCremation(cremationId);
        }

        public IEnumerable<Core.Domain.SubProductService> GetAvailableItemByCremation(int cremationId)
        {
            if (cremationId == 0)
                return new HashSet<Core.Domain.SubProductService>();

            var t = GetByCremation(cremationId);
            var sp = _subProductService.GetByProduct(_product.GetCremationProduct().Id);
            var f = sp.Where(s => !t.Any(y => y.SubProductServiceId == s.Id));

            return f;
        }

        public int Add(Core.Domain.CremationItem cremationItem)
        {
            _unitOfWork.CremationItems.Add(cremationItem);
            _unitOfWork.Complete();

            return cremationItem.Id;
        }

        public bool Change(int id, Core.Domain.CremationItem cremationItem)
        {
            var cremationItemInDB = GetById(id);

            if ((cremationItemInDB.CremationId != cremationItem.CremationId
                || cremationItemInDB.isOrder != cremationItem.isOrder)
                && _unitOfWork.CremationTransactions.Find(ct => ct.CremationItemId == cremationItemInDB.Id).Any())
            {
                return false;
            }

            cremationItemInDB.Price = cremationItem.Price;
            cremationItemInDB.Code = cremationItem.Code;
            cremationItemInDB.isOrder = cremationItem.isOrder;
            _unitOfWork.Complete();

            return true;
        }

        public bool Remove(int id)
        {
            if (_unitOfWork.CremationTransactions.Find(ct => ct.CremationItemId == id).Any())
            {
                return false;
            }

            var cremationItemInDB = GetById(id);

            if (cremationItemInDB == null)
            {
                return false;
            }

            _unitOfWork.CremationItems.Remove(cremationItemInDB);
            _unitOfWork.Complete();

            return true;
        }
    }
}