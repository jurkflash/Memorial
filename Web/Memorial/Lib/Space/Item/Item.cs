using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Memorial.Core;
using Memorial.Core.Dtos;
using AutoMapper;

namespace Memorial.Lib.Space
{
    public class Item : IItem
    {
        private readonly IUnitOfWork _unitOfWork;
        private Core.Domain.SpaceItem _item;

        public Item(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void SetItem(int id)
        {
            _item = _unitOfWork.SpaceItems.GetActive(id);
        }

        public Core.Domain.SpaceItem GetItem()
        {
            return _item;
        }

        public SpaceItemDto GetItemDto()
        {
            return Mapper.Map<Core.Domain.SpaceItem, SpaceItemDto>(GetItem());
        }

        public Core.Domain.SpaceItem GetItem(int id)
        {
            return _unitOfWork.SpaceItems.GetActive(id);
        }

        public SpaceItemDto GetItemDto(int id)
        {
            return Mapper.Map<Core.Domain.SpaceItem, SpaceItemDto>(GetItem(id));
        }

        public IEnumerable<SpaceItemDto> GetItemDtos()
        {
            return Mapper.Map<IEnumerable<Core.Domain.SpaceItem>, IEnumerable<SpaceItemDto>>(_unitOfWork.SpaceItems.GetAllActive());
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

        public bool AllowDoubleBook()
        {
            return _item.AllowDoubleBook;
        }

        public bool AllowDeposit()
        {
            return _item.AllowDeposit;
        }

        public IEnumerable<Core.Domain.SpaceItem> GetItemBySpace(int spaceId)
        {
            return _unitOfWork.SpaceItems.GetBySpace(spaceId);
        }

        public IEnumerable<SpaceItemDto> GetItemDtosBySpace(int spaceId)
        {
            return Mapper.Map<IEnumerable<Core.Domain.SpaceItem>, IEnumerable<SpaceItemDto>>(GetItemBySpace(spaceId));
        }

        public bool Create(SpaceItemDto spaceItemDto)
        {
            _item = new Core.Domain.SpaceItem();
            Mapper.Map(spaceItemDto, _item);

            _item.CreateDate = DateTime.Now;

            _unitOfWork.SpaceItems.Add(_item);

            return true;
        }

        public bool Update(Core.Domain.SpaceItem spaceItem)
        {
            spaceItem.ModifyDate = DateTime.Now;

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