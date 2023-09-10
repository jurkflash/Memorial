using System.Collections.Generic;
using System.Linq;
using Memorial.Core;
using AutoMapper;

namespace Memorial.Lib.AncestralTablet
{
    public class Area : IArea
    {
        private readonly IUnitOfWork _unitOfWork;

        public Area(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Core.Domain.AncestralTabletArea GetById(int areaId)
        {
            return _unitOfWork.AncestralTabletAreas.GetActive(areaId);
        }

        public IEnumerable<Core.Domain.AncestralTabletArea> GetAll()
        {
            return _unitOfWork.AncestralTabletAreas.GetAllActive();
        }

        public IEnumerable<Core.Domain.AncestralTabletArea> GetBySite(int siteId)
        {
            return _unitOfWork.AncestralTabletAreas.GetBySite(siteId);
        }

        public int Add(Core.Domain.AncestralTabletArea ancestralTabletArea)
        {
            _unitOfWork.AncestralTabletAreas.Add(ancestralTabletArea);

            _unitOfWork.Complete();

            return ancestralTabletArea.Id;
        }

        public bool Change(int id, Core.Domain.AncestralTabletArea ancestralTabletArea)
        {
            var ancestralTabletAreaInDB = _unitOfWork.AncestralTabletAreas.GetActive(id);

            if (ancestralTabletAreaInDB.SiteId != ancestralTabletArea.SiteId
                && _unitOfWork.AncestralTabletTransactions.Find(at => at.AncestralTabletItem.AncestralTabletArea.SiteId == ancestralTabletAreaInDB.SiteId).Any())
            {
                return false;
            }

            ancestralTabletAreaInDB.Name = ancestralTabletArea.Name;
            ancestralTabletAreaInDB.Description = ancestralTabletArea.Description;
            ancestralTabletAreaInDB.Remark = ancestralTabletArea.Remark;
            _unitOfWork.Complete();

            return true;
        }

        public bool Remove(int id)
        {
            if (_unitOfWork.AncestralTabletTransactions.Find(at => at.AncestralTabletItem.AncestralTabletArea.SiteId == id).Any())
            {
                return false;
            }

            var ancestralTabletAreaInDB = _unitOfWork.AncestralTabletAreas.GetActive(id);

            if (ancestralTabletAreaInDB == null)
            {
                return false;
            }

            _unitOfWork.AncestralTabletAreas.Remove(ancestralTabletAreaInDB);
            
            _unitOfWork.Complete();

            return true;
        }
    }
}