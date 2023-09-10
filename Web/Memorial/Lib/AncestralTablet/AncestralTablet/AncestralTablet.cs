using System.Collections.Generic;
using System.Linq;
using Memorial.Core;
using AutoMapper;

namespace Memorial.Lib.AncestralTablet
{
    public class AncestralTablet : IAncestralTablet
    {
        private readonly IUnitOfWork _unitOfWork;

        public AncestralTablet(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Core.Domain.AncestralTablet GetById(int id)
        {
            return _unitOfWork.AncestralTablets.GetActive(id);
        }

        public IEnumerable<Core.Domain.AncestralTablet> GetByAreaId(int id)
        {
            return _unitOfWork.AncestralTablets.GetByArea(id);
        }

        public IEnumerable<Core.Domain.AncestralTablet> GetAvailableAncestralTabletsByAreaId(int id)
        {
            return _unitOfWork.AncestralTablets.GetAvailableByArea(id);
        }

        public Core.Domain.AncestralTablet GetByAreaIdAndPostions(int areaId, int positionX, int positionY)
        {
            return _unitOfWork.AncestralTablets.GetByAreaAndPositions(areaId, positionX, positionY);
        }

        public IDictionary<byte, IEnumerable<byte>> GetPositionsByAreaId(int areaId)
        {
            return _unitOfWork.AncestralTablets.GetPositionsByArea(areaId);
        }

        public int Add(Core.Domain.AncestralTablet ancestralTablet)
        {
            if (GetByAreaIdAndPostions(ancestralTablet.AncestralTabletAreaId, ancestralTablet.PositionX, ancestralTablet.PositionY) != null)
                return -1;
            _unitOfWork.AncestralTablets.Add(ancestralTablet);
            _unitOfWork.Complete();

            return ancestralTablet.Id;
        }

        public bool Change(int id, Core.Domain.AncestralTablet ancestralTablet)
        {
            var ancestralTabletInDB = GetById(id);

            if (ancestralTabletInDB.AncestralTabletAreaId != ancestralTablet.AncestralTabletAreaId
                && (ancestralTabletInDB.ApplicantId != null || ancestralTabletInDB.hasDeceased))
            {
                return false;
            }

            if ((ancestralTabletInDB.PositionX != ancestralTablet.PositionX || ancestralTabletInDB.PositionY != ancestralTablet.PositionY) &&
                GetByAreaIdAndPostions(ancestralTablet.AncestralTabletAreaId, ancestralTablet.PositionX, ancestralTablet.PositionY) != null)
                return false;

            ancestralTabletInDB.Name = ancestralTablet.Name;
            ancestralTabletInDB.PositionX = ancestralTablet.PositionX;
            ancestralTabletInDB.PositionY = ancestralTablet.PositionY;
            ancestralTabletInDB.Price = ancestralTablet.Price;
            ancestralTabletInDB.Maintenance = ancestralTablet.Maintenance;
            ancestralTabletInDB.Remark = ancestralTablet.Remark;
            _unitOfWork.Complete();

            return true;
        }

        public bool Remove(int id)
        {
            if (_unitOfWork.AncestralTabletTransactions.Find(at => (at.AncestralTabletId == id || at.ShiftedAncestralTabletId == id)).Any())
            {
                return false;
            }

            var ancestralTabletInDB = GetById(id);

            if (ancestralTabletInDB == null)
            {
                return false;
            }

            _unitOfWork.AncestralTablets.Remove(ancestralTabletInDB);

            _unitOfWork.Complete();

            return true;
        }
    }
}