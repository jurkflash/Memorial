using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Memorial.Core;
using Memorial.Core.Dtos;
using AutoMapper;

namespace Memorial.Lib.Quadrangle
{
    public class Item : IItem
    {
        private readonly IUnitOfWork _unitOfWork;
        private Core.Domain.QuadrangleItem _item;

        public Item(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void SetItem(int id)
        {
            _item = _unitOfWork.QuadrangleItems.GetActive(id);
        }

        public Core.Domain.QuadrangleItem GetItem()
        {
            return _item;
        }

        public QuadrangleItemDto GetItemDto()
        {
            return Mapper.Map<Core.Domain.QuadrangleItem, QuadrangleItemDto>(GetItem());
        }

        public Core.Domain.QuadrangleItem GetItem(int id)
        {
            return _unitOfWork.QuadrangleItems.GetActive(id);
        }

        public QuadrangleItemDto GetItemDto(int id)
        {
            return Mapper.Map<Core.Domain.QuadrangleItem, QuadrangleItemDto>(GetItem(id));
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

        public IEnumerable<Core.Domain.QuadrangleItem> GetItemByCentre(int centreId)
        {
            return _unitOfWork.QuadrangleItems.GetByCentre(centreId);
        }

        public IEnumerable<QuadrangleItemDto> GetItemDtosByCentre(int centreId)
        {
            return Mapper.Map<IEnumerable<Core.Domain.QuadrangleItem>, IEnumerable<QuadrangleItemDto>>(GetItemByCentre(centreId));
        }

        public bool Create(QuadrangleItemDto quadrangleItemDto)
        {
            _item = new Core.Domain.QuadrangleItem();
            Mapper.Map(quadrangleItemDto, _item);

            _item.CreateDate = DateTime.Now;

            _unitOfWork.QuadrangleItems.Add(_item);

            return true;
        }

        public bool Update(Core.Domain.QuadrangleItem quadrangleItem)
        {
            quadrangleItem.ModifyDate = DateTime.Now;

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