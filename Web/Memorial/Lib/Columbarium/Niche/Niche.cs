using System.Collections.Generic;
using System.Linq;
using Memorial.Core;
using AutoMapper;
using Memorial.Core.Domain;

namespace Memorial.Lib.Columbarium
{
    public class Niche : INiche
    {
        private readonly IUnitOfWork _unitOfWork;

        public Niche(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Core.Domain.Niche GetById(int id)
        {
            return _unitOfWork.Niches.GetActive(id);
        }

        public IEnumerable<Core.Domain.Niche> GetByAreaId(int id)
        {
            return _unitOfWork.Niches.GetByArea(id);
        }

        public IEnumerable<Core.Domain.Niche> GetAvailableNichesByAreaId(int id)
        {
            return _unitOfWork.Niches.GetAvailableByArea(id);
        }

        public IEnumerable<Core.Domain.Niche> GetByAreaIdAndTypeId(int areaId, int typeId, string filter)
        {
            return _unitOfWork.Niches.GetByTypeAndArea(areaId, typeId, filter);
        }

        public Core.Domain.Niche GetByAreaIdAndPostions(int areaId, int positionX, int positionY)
        {
            return _unitOfWork.Niches.GetByAreaAndPositions(areaId, positionX, positionY);
        }

        public IDictionary<byte, IEnumerable<byte>> GetPositionsByAreaId(int areaId)
        {
            return _unitOfWork.Niches.GetPositionsByArea(areaId);
        }

        public int Add(Core.Domain.Niche niche)
        {
            if (GetByAreaIdAndPostions(niche.ColumbariumAreaId, niche.PositionX, niche.PositionY) != null)
                return -1;

            _unitOfWork.Niches.Add(niche);

            _unitOfWork.Complete();

            return niche.Id;
        }

        public bool Change(int id, Core.Domain.Niche niche)
        {
            var nicheInDB = _unitOfWork.Niches.GetActive(id);

            if ((nicheInDB.NicheTypeId != niche.NicheTypeId
                || nicheInDB.ColumbariumAreaId != niche.ColumbariumAreaId)
                && _unitOfWork.ColumbariumTransactions.Find(qt => (qt.NicheId == niche.Id || qt.ShiftedNicheId == niche.Id)).Any())
            {
                return false;
            }

            if ((nicheInDB.PositionX != niche.PositionX || nicheInDB.PositionY != niche.PositionY) && GetByAreaIdAndPostions(niche.ColumbariumAreaId, niche.PositionX, niche.PositionY) != null)
                return false;

            nicheInDB.Name = niche.Name;
            nicheInDB.Description = niche.Description;
            nicheInDB.PositionX = niche.PositionX;
            nicheInDB.PositionY = niche.PositionY;
            nicheInDB.Price = niche.Price;
            nicheInDB.Maintenance = niche.Maintenance;
            nicheInDB.LifeTimeMaintenance = niche.LifeTimeMaintenance;
            nicheInDB.Remark = niche.Remark;
            _unitOfWork.Complete();

            return true;
        }

        public bool Remove(int id)
        {
            if (_unitOfWork.ColumbariumTransactions.Find(qt => (qt.NicheId == id || qt.ShiftedNicheId == id)).Any())
            {
                return false;
            }

            var nicheInDb = _unitOfWork.Niches.GetActive(id);

            if (nicheInDb == null)
            {
                return false;
            }

            _unitOfWork.Niches.Remove(nicheInDb);

            _unitOfWork.Complete();

            return true;
        }
    }
}