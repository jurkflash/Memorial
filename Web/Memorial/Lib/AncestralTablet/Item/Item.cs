using System;
using System.Collections.Generic;
using Memorial.Core;
using Memorial.Core.Dtos;
using AutoMapper;

namespace Memorial.Lib.AncestralTablet
{
    public class Item : IItem
    {
        private readonly IUnitOfWork _unitOfWork;
        private Core.Domain.AncestralTabletItem _item;

        public Item(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
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
            return _item.Name;
        }

        public string GetDescription()
        {
            return _item.Description;
        }

        public float GetPrice()
        {
            return _item.Price;
        }

        public string GetSystemCode()
        {
            return _item.SystemCode;
        }

        public bool IsOrder()
        {
            return _item.isOrder;
        }

        public IEnumerable<Core.Domain.AncestralTabletItem> GetItemByArea(int areaId)
        {
            return _unitOfWork.AncestralTabletItems.GetByArea(areaId);
        }

        public IEnumerable<AncestralTabletItemDto> GetItemDtosByArea(int areaId)
        {
            return Mapper.Map<IEnumerable<Core.Domain.AncestralTabletItem>, IEnumerable<AncestralTabletItemDto>>(GetItemByArea(areaId));
        }

        public bool Create(AncestralTabletItemDto ancestralTabletItemDto)
        {
            _item = new Core.Domain.AncestralTabletItem();
            Mapper.Map(ancestralTabletItemDto, _item);

            _item.CreateDate = DateTime.Now;

            _unitOfWork.AncestralTabletItems.Add(_item);

            return true;
        }

        public bool Update(Core.Domain.AncestralTabletItem ancestralTabletItem)
        {
            ancestralTabletItem.ModifyDate = DateTime.Now;

            return true;
        }

        public bool Delete(int id)
        {
            SetItem(id);

            _item.DeleteDate = DateTime.Now;

            return true;
        }

    }
}