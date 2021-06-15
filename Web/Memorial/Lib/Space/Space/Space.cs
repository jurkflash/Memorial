using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Memorial.Core;
using Memorial.Core.Dtos;
using Memorial.Lib.Space;
using AutoMapper;

namespace Memorial.Lib.Space
{
    public class Space : ISpace
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IItem _item;
        private Core.Domain.Space _space;

        public Space(IUnitOfWork unitOfWork, IItem item)
        {
            _unitOfWork = unitOfWork;
            _item = item;
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

        public IEnumerable<Core.Domain.Space> GetSpacesBySite(int siteId)
        {
            return _unitOfWork.Spaces.GetBySite(siteId);
        }

        public IEnumerable<SpaceDto> GetSpaceDtosBySite(int siteId)
        {
            return Mapper.Map<IEnumerable<Core.Domain.Space>, IEnumerable<SpaceDto>>(GetSpacesBySite(siteId));
        }

        public double GetAmount(DateTime from, DateTime to, int spaceItemId)
        {
            if (from > to)
                return -1;

            _item.SetItem(spaceItemId);
            
            if (_item.GetItem() == null)
                return -1;

            var diff = Math.Ceiling(((to - from).TotalMinutes / 60.0) / 24.0) * _item.GetPrice();

            return diff;
        }

        public bool CheckAvailability(DateTime from, DateTime to, int spaceItemId)
        {
            if (from > to)
                return false;

            _item.SetItem(spaceItemId);

            if (_item.GetItem() == null)
                return false;

            return _unitOfWork.SpaceTransactions.GetAvailability(from, to, spaceItemId);
        }

        public bool CheckAvailability(DateTime from, DateTime to, string AF)
        {
            if (from > to)
                return false;

            return _unitOfWork.SpaceTransactions.GetAvailability(from, to, AF);
        }

        public int Create(SpaceDto spaceDto)
        {
            _space = new Core.Domain.Space();
            Mapper.Map(spaceDto, _space);

            _space.CreateDate = DateTime.Now;

            _unitOfWork.Spaces.Add(_space);

            _unitOfWork.Complete();

            return _space.Id;
        }

        public bool Update(SpaceDto spaceDto)
        {
            var spaceInDB = GetSpace(spaceDto.Id);

            if (spaceInDB.SiteId != spaceDto.SiteDtoId
                && _unitOfWork.SpaceTransactions.Find(ct => ct.SpaceItem.Space.SiteId == spaceInDB.SiteId && ct.DeleteDate == null).Any())
            {
                return false;
            }

            Mapper.Map(spaceDto, spaceInDB);

            spaceInDB.ModifyDate = DateTime.Now;

            _unitOfWork.Complete();

            return true;
        }

        public bool Delete(int id)
        {
            if (_unitOfWork.SpaceTransactions.Find(ct => ct.SpaceItem.Space.Id == id && ct.DeleteDate == null).Any())
            {
                return false;
            }

            SetSpace(id);

            _space.DeleteDate = DateTime.Now;

            _unitOfWork.Complete();

            return true;
        }
    }
}