using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Memorial.Core;
using Memorial.Core.Dtos;
using AutoMapper;

namespace Memorial.Lib.Columbarium
{
    public class Item : IItem
    {
        private readonly IUnitOfWork _unitOfWork;
        private Core.Domain.ColumbariumItem _item;

        public Item(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void SetItem(int id)
        {
            _item = _unitOfWork.ColumbariumItems.GetActive(id);
        }

        public Core.Domain.ColumbariumItem GetItem()
        {
            return _item;
        }

        public ColumbariumItemDto GetItemDto()
        {
            return Mapper.Map<Core.Domain.ColumbariumItem, ColumbariumItemDto>(GetItem());
        }

        public Core.Domain.ColumbariumItem GetItem(int id)
        {
            return _unitOfWork.ColumbariumItems.GetActive(id);
        }

        public ColumbariumItemDto GetItemDto(int id)
        {
            return Mapper.Map<Core.Domain.ColumbariumItem, ColumbariumItemDto>(GetItem(id));
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

        public IEnumerable<Core.Domain.ColumbariumItem> GetItemByCentre(int centreId)
        {
            return _unitOfWork.ColumbariumItems.GetByCentre(centreId);
        }

        public IEnumerable<ColumbariumItemDto> GetItemDtosByCentre(int centreId)
        {
            return Mapper.Map<IEnumerable<Core.Domain.ColumbariumItem>, IEnumerable<ColumbariumItemDto>>(GetItemByCentre(centreId));
        }

        public bool Create(ColumbariumItemDto columbariumItemDto)
        {
            _item = new Core.Domain.ColumbariumItem();
            Mapper.Map(columbariumItemDto, _item);

            _item.CreateDate = DateTime.Now;

            _unitOfWork.ColumbariumItems.Add(_item);

            return true;
        }

        public bool Update(Core.Domain.ColumbariumItem columbariumItem)
        {
            columbariumItem.ModifyDate = DateTime.Now;

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