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

        public IEnumerable<UrnItemDto> GetItemDtos()
        {
            return Mapper.Map<IEnumerable<Core.Domain.UrnItem>, IEnumerable<UrnItemDto>>(_unitOfWork.UrnItems.GetAllActive());
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

        public IEnumerable<Core.Domain.UrnItem> GetItemByUrn(int urnId)
        {
            return _unitOfWork.UrnItems.GetByUrn(urnId);
        }

        public IEnumerable<UrnItemDto> GetItemDtosByUrn(int urnId)
        {
            return Mapper.Map<IEnumerable<Core.Domain.UrnItem>, IEnumerable<UrnItemDto>>(GetItemByUrn(urnId));
        }

        public bool Create(UrnItemDto urnItemDto)
        {
            _item = new Core.Domain.UrnItem();
            Mapper.Map(urnItemDto, _item);

            _item.CreateDate = DateTime.Now;

            _unitOfWork.UrnItems.Add(_item);

            return true;
        }

        public bool Update(Core.Domain.UrnItem urnItem)
        {
            urnItem.ModifyDate = DateTime.Now;

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