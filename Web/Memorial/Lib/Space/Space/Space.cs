using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Memorial.Core;
using Memorial.Core.Dtos;
using AutoMapper;

namespace Memorial.Lib.Space
{
    public class Space : ISpace
    {
        private readonly IUnitOfWork _unitOfWork;
        private Core.Domain.Space _space;

        public Space(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void SetSpace(int id)
        {
            _space = _unitOfWork.Spaces.GetActive(id);
        }

        public Core.Domain.Space GetSpace()
        {
            return _space;
        }

        public Core.Domain.Space GetSpace(int id)
        {
            return _unitOfWork.Spaces.GetActive(id);
        }

        public SpaceDto GetSpaceDto(int id)
        {
            return Mapper.Map<Core.Domain.Space, SpaceDto>(GetSpace(id));
        }

        public SpaceDto GetSpaceDto()
        {
            return Mapper.Map<Core.Domain.Space, SpaceDto>(GetSpace());
        }

        public IEnumerable<SpaceDto> GetSpaceDtos()
        {
            return Mapper.Map<IEnumerable<Core.Domain.Space>, IEnumerable<SpaceDto>>(_unitOfWork.Spaces.GetAllActive());
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

        public bool CheckAvailability(DateTime from, DateTime to, string AF)
        {
            if (from > to)
                return false;

            return _unitOfWork.SpaceTransactions.GetAvailability(from, to, AF);
        }

        public bool Create(SpaceDto spaceDto)
        {
            _space = new Core.Domain.Space();
            Mapper.Map(spaceDto, _space);

            _space.CreateDate = DateTime.Now;

            _unitOfWork.Spaces.Add(_space);

            return true;
        }

        public bool Update(Core.Domain.Space space)
        {
            space.ModifyDate = DateTime.Now;

            return true;
        }

        public bool Delete(int id)
        {
            SetSpace(id);

            _space.DeleteDate = DateTime.Now;

            return true;
        }
    }
}