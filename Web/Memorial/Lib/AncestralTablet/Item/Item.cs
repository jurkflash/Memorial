using System;
using System.Collections.Generic;
using Memorial.Core;
using Memorial.Core.Dtos;
using AutoMapper;

namespace Memorial.Lib.Ancestor
{
    public class Item : IItem
    {
        private readonly IUnitOfWork _unitOfWork;
        private Core.Domain.AncestorItem _item;

        public Item(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void SetItem(int id)
        {
            _item = _unitOfWork.AncestorItems.GetActive(id);
        }

        public Core.Domain.AncestorItem GetItem()
        {
            return _item;
        }

        public AncestorItemDto GetItemDto()
        {
            return Mapper.Map<Core.Domain.AncestorItem, AncestorItemDto>(GetItem());
        }

        public Core.Domain.AncestorItem GetItem(int id)
        {
            return _unitOfWork.AncestorItems.GetActive(id);
        }

        public AncestorItemDto GetItemDto(int id)
        {
            return Mapper.Map<Core.Domain.AncestorItem, AncestorItemDto>(GetItem(id));
        }

        public IEnumerable<Core.Domain.AncestorItem> GetItems()
        {
            return _unitOfWork.AncestorItems.GetAllActive();
        }

        public IEnumerable<AncestorItemDto> GetItemDtos()
        {
            return Mapper.Map<IEnumerable<Core.Domain.AncestorItem>, IEnumerable<AncestorItemDto>>(GetItems());
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

        public IEnumerable<Core.Domain.AncestorItem> GetItemByArea(int areaId)
        {
            return _unitOfWork.AncestorItems.GetByArea(areaId);
        }

        public IEnumerable<AncestorItemDto> GetItemDtosByArea(int areaId)
        {
            return Mapper.Map<IEnumerable<Core.Domain.AncestorItem>, IEnumerable<AncestorItemDto>>(GetItemByArea(areaId));
        }

        public bool Create(AncestorItemDto ancestorItemDto)
        {
            _item = new Core.Domain.AncestorItem();
            Mapper.Map(ancestorItemDto, _item);

            _item.CreateDate = DateTime.Now;

            _unitOfWork.AncestorItems.Add(_item);

            return true;
        }

        public bool Update(Core.Domain.AncestorItem ancestorItem)
        {
            ancestorItem.ModifyDate = DateTime.Now;

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