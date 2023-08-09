using System;
using System.Collections.Generic;
using Memorial.Core;
using Memorial.Core.Dtos;
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
        private Core.Domain.AncestralTabletItem _item;

        public Item(IUnitOfWork unitOfWork, IProduct product, ISubProductService subProductService)
        {
            _unitOfWork = unitOfWork;
            _product = product;
            _subProductService = subProductService;
        }

        public void SetItem(int id)
        {
            _item = _unitOfWork.AncestralTabletItems.GetActive(id);
        }

        public Core.Domain.AncestralTabletItem GetItem()
        {
            return _item;
        }

        public AncestralTabletItemDto GetItemDto()
        {
            return Mapper.Map<Core.Domain.AncestralTabletItem, AncestralTabletItemDto>(GetItem());
        }

        public Core.Domain.AncestralTabletItem GetItem(int id)
        {
            return _unitOfWork.AncestralTabletItems.GetActive(id);
        }

        public AncestralTabletItemDto GetItemDto(int id)
        {
            return Mapper.Map<Core.Domain.AncestralTabletItem, AncestralTabletItemDto>(GetItem(id));
        }

        public IEnumerable<Core.Domain.AncestralTabletItem> GetItems()
        {
            return _unitOfWork.AncestralTabletItems.GetAllActive();
        }

        public IEnumerable<AncestralTabletItemDto> GetItemDtos()
        {
            return Mapper.Map<IEnumerable<Core.Domain.AncestralTabletItem>, IEnumerable<AncestralTabletItemDto>>(GetItems());
        }

        public int GetId()
        {
            return _item.Id;
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

        public IEnumerable<Core.Domain.AncestralTabletItem> GetItemByArea(int areaId)
        {
            return _unitOfWork.AncestralTabletItems.GetByArea(areaId);
        }

        public IEnumerable<AncestralTabletItemDto> GetItemDtosByArea(int areaId)
        {
            return Mapper.Map<IEnumerable<Core.Domain.AncestralTabletItem>, IEnumerable<AncestralTabletItemDto>>(GetItemByArea(areaId));
        }

        public IEnumerable<SubProductServiceDto> GetAvailableItemDtosByArea(int areaId)
        {
            if (areaId == 0)
                return new HashSet<SubProductServiceDto>();

            var t = GetItemByArea(areaId);
            var sp = _subProductService.GetByProduct(_product.GetAncestralTabletProduct().Id);
            var f = sp.Where(s => !t.Any(y => y.SubProductServiceId == s.Id));

            return Mapper.Map<IEnumerable<Core.Domain.SubProductService>, IEnumerable<SubProductServiceDto>>(f);
        }

        public int Create(AncestralTabletItemDto ancestralTabletItemDto)
        {
            _item = new Core.Domain.AncestralTabletItem();
            Mapper.Map(ancestralTabletItemDto, _item);

            _unitOfWork.AncestralTabletItems.Add(_item);

            _unitOfWork.Complete();

            return _item.Id;
        }

        public bool Update(AncestralTabletItemDto ancestralTabletItemDto)
        {
            var ancestralTabletItemInDB = GetItem(ancestralTabletItemDto.Id);

            if ((ancestralTabletItemInDB.AncestralTabletAreaId != ancestralTabletItemDto.AncestralTabletAreaDtoId
                || ancestralTabletItemInDB.isOrder != ancestralTabletItemDto.isOrder)
                && _unitOfWork.AncestralTabletTransactions.Find(at => at.AncestralTabletItemId == ancestralTabletItemInDB.Id).Any())
            {
                return false;
            }

            Mapper.Map(ancestralTabletItemDto, ancestralTabletItemInDB);

            _unitOfWork.Complete();

            return true;
        }

        public bool Delete(int id)
        {
            if (_unitOfWork.AncestralTabletTransactions.Find(at => at.AncestralTabletItemId == id).Any())
            {
                return false;
            }

            SetItem(id);

            if (_item == null)
            {
                return false;
            }

            _unitOfWork.AncestralTabletItems.Remove(_item);
            
            _unitOfWork.Complete();

            return true;
        }

    }
}