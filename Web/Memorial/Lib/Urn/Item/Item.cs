using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Memorial.Core;
using Memorial.Core.Dtos;
using AutoMapper;

namespace Memorial.Lib.Urn
{
    public class Item : IItem
    {
        private readonly IUnitOfWork _unitOfWork;
        private Core.Domain.UrnItem _item;

        public Item(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void SetItem(int id)
        {
            _item = _unitOfWork.UrnItems.GetActive(id);
        }

        public Core.Domain.UrnItem GetItem()
        {
            return _item;
        }

        public UrnItemDto GetItemDto()
        {
            return Mapper.Map<Core.Domain.UrnItem, UrnItemDto>(GetItem());
        }

        public Core.Domain.UrnItem GetItem(int id)
        {
            return _unitOfWork.UrnItems.GetActive(id);
        }

        public UrnItemDto GetItemDto(int id)
        {
            return Mapper.Map<Core.Domain.UrnItem, UrnItemDto>(GetItem(id));
        }

        public int GetId()
        {
            return _item.Id;
        }

        public int GetUrnId()
        {
            return _item.UrnId;
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

        public IEnumerable<Core.Domain.UrnItem> GetItemByUrn(int urnId)
        {
            return _unitOfWork.UrnItems.GetByUrn(urnId);
        }

        public IEnumerable<UrnItemDto> GetItemDtosByUrn(int urnId)
        {
            return Mapper.Map<IEnumerable<Core.Domain.UrnItem>, IEnumerable<UrnItemDto>>(GetItemByUrn(urnId));
        }

    }
}