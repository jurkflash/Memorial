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

        public IEnumerable<Core.Domain.QuadrangleItem> GetByCentre(int centreId)
        {
            return _unitOfWork.QuadrangleItems.GetByCentre(centreId);
        }

        public IEnumerable<QuadrangleItemDto> DtosGetByCentre(int centreId)
        {
            return Mapper.Map<IEnumerable<Core.Domain.QuadrangleItem>, IEnumerable<QuadrangleItemDto>>(GetByCentre(centreId));
        }

    }
}