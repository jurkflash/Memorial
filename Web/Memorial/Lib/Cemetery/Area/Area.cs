using System.Collections.Generic;
using System.Linq;
using Memorial.Core;
using AutoMapper;

namespace Memorial.Lib.Cemetery
{
    public class Area : IArea
    {
        private readonly IUnitOfWork _unitOfWork;

        public Area(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Core.Domain.CemeteryArea GetById(int areaId)
        {
            return _unitOfWork.CemeteryAreas.GetActive(areaId);
        }

        public IEnumerable<Core.Domain.CemeteryArea> GetAll()
        {
            return _unitOfWork.CemeteryAreas.GetAllActive();
        }

        public IEnumerable<Core.Domain.CemeteryArea> GetBySite(int siteId)
        {
            return _unitOfWork.CemeteryAreas.GetBySite(siteId);
        }

        public int Add(Core.Domain.CemeteryArea cemeteryArea)
        {
            _unitOfWork.CemeteryAreas.Add(cemeteryArea);
            _unitOfWork.Complete();

            return cemeteryArea.Id;
        }

        public bool Change(int id, Core.Domain.CemeteryArea cemeteryArea)
        {
            var cemeteryAreaInDB = _unitOfWork.CemeteryAreas.Get(id);

            if (cemeteryAreaInDB.SiteId != cemeteryArea.SiteId
                && _unitOfWork.CemeteryTransactions.Find(ct => ct.Plot.CemeteryAreaId == cemeteryAreaInDB.Id).Any())
            {
                return false;
            }

            cemeteryAreaInDB.Name = cemeteryArea.Name;
            cemeteryAreaInDB.Description = cemeteryArea.Description;
            _unitOfWork.Complete();

            return true;
        }

        public bool Remove(int id)
        {
            if (_unitOfWork.CemeteryTransactions.Find(ct => ct.Plot.CemeteryAreaId == id).Any())
            {
                return false;
            }

            var cemeteryAreaInDB = _unitOfWork.CemeteryAreas.Get(id);
            if (cemeteryAreaInDB == null)
            {
                return false;
            }

            _unitOfWork.CemeteryAreas.Remove(cemeteryAreaInDB);
            _unitOfWork.Complete();
            return true;
        }
    }
}