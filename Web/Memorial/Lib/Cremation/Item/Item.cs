using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Memorial.Core;
using Memorial.Core.Repositories;
using Memorial.Core.Dtos;
using AutoMapper;

namespace Memorial.Lib.Cremation
{
    public class Item : IItem
    {
        private readonly IUnitOfWork _unitOfWork;
        private Core.Domain.CremationItem _item;

        public Item(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
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

        public IEnumerable<Core.Domain.CremationItem> GetItemByCremation(int cremationId)
        {
            return _unitOfWork.CremationItems.GetByCremation(cremationId);
        }

        public IEnumerable<CremationItemDto> GetItemDtosByCremation(int cremationId)
        {
            return Mapper.Map<IEnumerable<Core.Domain.CremationItem>, IEnumerable<CremationItemDto>>(GetItemByCremation(cremationId));
        }
    }
}