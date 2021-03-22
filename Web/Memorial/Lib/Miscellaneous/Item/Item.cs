using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Memorial.Core;
using Memorial.Core.Repositories;
using Memorial.Core.Dtos;
using AutoMapper;

namespace Memorial.Lib.Miscellaneous
{
    public class Item : IItem
    {
        private readonly IUnitOfWork _unitOfWork;
        private Core.Domain.MiscellaneousItem _item;

        public Item(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void SetItem(int id)
        {
            _item = _unitOfWork.MiscellaneousItems.GetActive(id);
        }

        public Core.Domain.MiscellaneousItem GetItem()
        {
            return _item;
        }

        public MiscellaneousItemDto GetItemDto()
        {
            return Mapper.Map<Core.Domain.MiscellaneousItem, MiscellaneousItemDto>(GetItem());
        }

        public Core.Domain.MiscellaneousItem GetItem(int id)
        {
            return _unitOfWork.MiscellaneousItems.GetActive(id);
        }

        public MiscellaneousItemDto GetItemDto(int id)
        {
            return Mapper.Map<Core.Domain.MiscellaneousItem, MiscellaneousItemDto>(GetItem(id));
        }

        public int GetId()
        {
            return _item.Id;
        }

        public int GetMiscellaneousId()
        {
            return _item.MiscellaneousId;
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

        public IEnumerable<Core.Domain.MiscellaneousItem> GetItemByMiscellaneous(int miscellaneousId)
        {
            return _unitOfWork.MiscellaneousItems.GetByMiscellaneous(miscellaneousId);
        }

        public IEnumerable<MiscellaneousItemDto> GetItemDtosByMiscellaneous(int miscellaneousId)
        {
            return Mapper.Map<IEnumerable<Core.Domain.MiscellaneousItem>, IEnumerable<MiscellaneousItemDto>>(GetItemByMiscellaneous(miscellaneousId));
        }
    }
}