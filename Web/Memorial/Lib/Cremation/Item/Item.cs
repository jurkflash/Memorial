using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Memorial.Core;
using Memorial.Core.Repositories;
using Memorial.Core.Dtos;
using Memorial.Lib.Product;
using Memorial.Lib.SubProductService;
using AutoMapper;

namespace Memorial.Lib.Cremation
{
    public class Item : IItem
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IProduct _product;
        private readonly ISubProductService _subProductService;
        private Core.Domain.CremationItem _item;

        public Item(IUnitOfWork unitOfWork, IProduct product, ISubProductService subProductService)
        {
            _unitOfWork = unitOfWork;
            _product = product;
            _subProductService = subProductService;
        }

        public void SetItem(int id)
        {
            _item = _unitOfWork.CremationItems.GetActive(id);
        }

        public Core.Domain.CremationItem GetItem()
        {
            return _item;
        }

        public CremationItemDto GetItemDto()
        {
            return Mapper.Map<Core.Domain.CremationItem, CremationItemDto>(GetItem());
        }

        public Core.Domain.CremationItem GetItem(int id)
        {
            return _unitOfWork.CremationItems.GetActive(id);
        }

        public CremationItemDto GetItemDto(int id)
        {
            return Mapper.Map<Core.Domain.CremationItem, CremationItemDto>(GetItem(id));
        }

        public IEnumerable<CremationItemDto> GetItemDtos()
        {
            return Mapper.Map<IEnumerable<Core.Domain.CremationItem>, IEnumerable<CremationItemDto>>(_unitOfWork.CremationItems.GetAllActive());
        }

        public int GetId()
        {
            return _item.Id;
        }

        public int GetCremationId()
        {
            return _item.CremationId;
        }

        public string GetName()
        {
            return _item.SubProductService.Name;
        }

        public string GetDescription()
        {
            return _item.SubProductService.Description;
        }

        public float GetPrice()
        {
            if (_item.Price.HasValue)
                return _item.Price.Value;
            else
                return _item.SubProductService.Price;
        }

        public string GetSystemCode()
        {
            return _item.SubProductService.SystemCode;
        }

        public bool IsOrder()
        {
            if (_item.isOrder.HasValue)
                return _item.isOrder.Value;
            else
                return _item.SubProductService.isOrder;
        }

        public IEnumerable<Core.Domain.CremationItem> GetItemByCremation(int cremationId)
        {
            return _unitOfWork.CremationItems.GetByCremation(cremationId);
        }

        public IEnumerable<CremationItemDto> GetItemDtosByCremation(int cremationId)
        {
            return Mapper.Map<IEnumerable<Core.Domain.CremationItem>, IEnumerable<CremationItemDto>>(GetItemByCremation(cremationId));
        }

        public IEnumerable<SubProductServiceDto> GetAvailableItemDtosByCremation(int cremationId)
        {
            if (cremationId == 0)
                return new HashSet<SubProductServiceDto>();

            var t = GetItemByCremation(cremationId);
            var sp = _subProductService.GetSubProductServicesByProduct(_product.GetCremationProduct().Id);
            var f = sp.Where(s => !t.Any(y => y.SubProductServiceId == s.Id && y.DeleteDate == null));

            return Mapper.Map<IEnumerable<Core.Domain.SubProductService>, IEnumerable<SubProductServiceDto>>(f);
        }

        public int Create(CremationItemDto cremationItemDto)
        {
            _item = new Core.Domain.CremationItem();
            Mapper.Map(cremationItemDto, _item);

            _item.CreatedDate = DateTime.Now;

            _unitOfWork.CremationItems.Add(_item);

            _unitOfWork.Complete();

            return _item.Id;
        }

        public bool Update(CremationItemDto cremationItemDto)
        {
            var cremationItemInDB = GetItem(cremationItemDto.Id);

            if ((cremationItemInDB.CremationId != cremationItemDto.CremationDtoId
                || cremationItemInDB.isOrder != cremationItemDto.isOrder)
                && _unitOfWork.CremationTransactions.Find(ct => ct.CremationItemId == cremationItemInDB.Id && ct.DeleteDate == null).Any())
            {
                return false;
            }

            Mapper.Map(cremationItemDto, cremationItemInDB);

            cremationItemInDB.ModifiedDate = DateTime.Now;

            _unitOfWork.Complete();

            return true;
        }

        public bool Delete(int id)
        {
            if (_unitOfWork.CremationTransactions.Find(ct => ct.CremationItemId == id && ct.DeleteDate == null).Any())
            {
                return false;
            }

            SetItem(id);

            _item.DeleteDate = DateTime.Now;

            _unitOfWork.Complete();

            return true;
        }
    }
}