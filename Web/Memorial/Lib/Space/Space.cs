using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Memorial.Core;
using Memorial.Core.Dtos;
using AutoMapper;

namespace Memorial.Lib
{
    public class Space : ISpace
    {
        private Memorial.Core.Domain.SpaceTransaction _spaceTransaction { get; set; }

        private readonly IUnitOfWork _unitOfWork;

        public Space(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<SpaceDto> DtosGetBySite(byte siteId)
        {
            return Mapper.Map<IEnumerable<Core.Domain.Space>, IEnumerable<SpaceDto>>(_unitOfWork.Spaces.GetBySite(siteId));
        }

        public double GetAmount(DateTime from, DateTime to, int spaceItemId)
        {
            if (from > to)
                return -1;

            var spaceItem = _unitOfWork.SpaceItems.Get(spaceItemId);

            if (spaceItem == null)
                return -1;

            var diff = Math.Ceiling(((to - from).TotalMinutes / 60.0) / 24.0) * spaceItem.Price;

            return diff;
        }

        public bool CheckAvailability(DateTime from, DateTime to, int spaceItemId)
        {
            if (from > to)
                return false;

            var spaceItem = _unitOfWork.SpaceItems.Get(spaceItemId);

            if (spaceItem == null)
                return false;

            return _unitOfWork.SpaceTransactions.GetAvailability(from, to, spaceItemId);
        }

    }
}