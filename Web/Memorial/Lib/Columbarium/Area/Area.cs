using System.Collections.Generic;
using System.Linq;
using Memorial.Core;
using AutoMapper;
using Memorial.Core.Domain;

namespace Memorial.Lib.Columbarium
{
    public class Area : IArea
    {
        private readonly IUnitOfWork _unitOfWork;

        public Area(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Core.Domain.ColumbariumArea GetById(int areaId)
        {
            return _unitOfWork.ColumbariumAreas.GetActive(areaId);
        }

        public IEnumerable<Core.Domain.ColumbariumArea> GetByCentre(int centreId)
        {
            return _unitOfWork.ColumbariumAreas.GetByCentre(centreId);
        }

        public int Add(Core.Domain.ColumbariumArea columbariumArea)
        {
            _unitOfWork.ColumbariumAreas.Add(columbariumArea);

            _unitOfWork.Complete();

            return columbariumArea.Id;
        }

        public bool Change(int id, Core.Domain.ColumbariumArea columbariumArea)
        {
            var columbariumAreaInDB = _unitOfWork.ColumbariumAreas.GetActive(id);

            if (columbariumAreaInDB.ColumbariumCentreId != columbariumArea.ColumbariumCentreId
                && _unitOfWork.ColumbariumTransactions.Find(qt => (qt.Niche.ColumbariumAreaId == columbariumAreaInDB.Id || qt.ShiftedNiche.ColumbariumAreaId == columbariumAreaInDB.Id)).Any())
            {
                return false;
            }

            columbariumAreaInDB.Name = columbariumArea.Name;
            columbariumAreaInDB.Description = columbariumArea.Description;
            _unitOfWork.Complete();

            return true;
        }

        public bool Remove(int id)
        {
            if (_unitOfWork.ColumbariumTransactions.Find(qt => (qt.Niche.ColumbariumAreaId == id || qt.ShiftedNiche.ColumbariumAreaId == id)).Any())
            {
                return false;
            }

            var areaInDb = _unitOfWork.ColumbariumAreas.GetActive(id);

            if (areaInDb == null)
            {
                return false;
            }

            _unitOfWork.ColumbariumAreas.Remove(areaInDb);

            _unitOfWork.Complete();

            return true;
        }
    }
}