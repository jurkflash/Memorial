using System;
using System.Collections.Generic;
using System.Linq;
using Memorial.Core;
using AutoMapper;

namespace Memorial.Lib.Space
{
    public class Space : ISpace
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IItem _item;

        public Space(IUnitOfWork unitOfWork, IItem item)
        {
            _unitOfWork = unitOfWork;
            _item = item;
        }

        public Core.Domain.Space Get(int id)
        {
            return _unitOfWork.Spaces.GetActive(id);
        }

        public IEnumerable<Core.Domain.Space> GetBySite(int siteId)
        {
            return _unitOfWork.Spaces.GetBySite(siteId);
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

        public int Add(Core.Domain.Space space)
        {
            _unitOfWork.Spaces.Add(space);

            _unitOfWork.Complete();

            return space.Id;
        }

        public bool Change(int spaceId, Core.Domain.Space space)
        {
            var spaceInDB = _unitOfWork.Spaces.Get(spaceId);

            if (spaceInDB.SiteId != space.SiteId
                && _unitOfWork.SpaceTransactions.Find(ct => ct.SpaceItem.Space.SiteId == spaceInDB.SiteId).Any())
            {
                return false;
            }

            Mapper.Map(space, spaceInDB);

            _unitOfWork.Complete();

            return true;
        }

        public bool Remove(int id)
        {
            if (_unitOfWork.SpaceTransactions.Find(ct => ct.SpaceItem.Space.Id == id).Any())
            {
                return false;
            }

            var spaceInDB = _unitOfWork.Spaces.Get(id);

            if (spaceInDB == null)
            {
                return false;
            }

            _unitOfWork.Spaces.Remove(spaceInDB);

            _unitOfWork.Complete();

            return true;
        }
    }
}