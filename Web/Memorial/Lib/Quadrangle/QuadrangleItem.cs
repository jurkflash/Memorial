using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Memorial.Core;
using Memorial.Core.Dtos;
using AutoMapper;

namespace Memorial.Lib
{
    public class QuadrangleItem : IQuadrangleItem
    {
        private readonly IUnitOfWork _unitOfWork;
        private Core.Domain.QuadrangleItem _quadrangleItem;

        public QuadrangleItem(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void SetById(int id)
        {
            _quadrangleItem = _unitOfWork.QuadrangleItems.GetActive(id);
        }

        public string GetSystemCode()
        {
            return _quadrangleItem.SystemCode;
        }

        public IEnumerable<Core.Domain.QuadrangleItem> GetByCentre(int centreId)
        {
            return _unitOfWork.QuadrangleItems.GetByCentre(centreId);
        }

        public IEnumerable<QuadrangleItemDto> DtosGetByCentre(int centreId)
        {
            return Mapper.Map<IEnumerable<Core.Domain.QuadrangleItem>, IEnumerable<QuadrangleItemDto>>(GetByCentre(centreId));
        }

        public bool GetOrderFlag()
        {
            return _quadrangleItem.isOrder;
        }

        public float GetPrice()
        {
            return _quadrangleItem.Price;
        }

        public float GetAmount(DateTime from, DateTime to, int itemId)
        {
            if (from > to)
                return -1;

            var quadrangleItem = _unitOfWork.QuadrangleItems.GetActive(itemId);

            if (quadrangleItem == null)
                return -1;

            var diff = (((to.Year - from.Year) * 12) + to.Month - from.Month) * quadrangleItem.Price;

            return diff;
        }
    }
}