using System.Collections.Generic;
using System.Linq;
using Memorial.Core;
using AutoMapper;

namespace Memorial.Lib.Columbarium
{
    public class Centre : ICentre
    {
        private readonly IUnitOfWork _unitOfWork;

        public Centre(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Core.Domain.ColumbariumCentre GetById(int id)
        {
            return _unitOfWork.ColumbariumCentres.GetActive(id);
        }

        public IEnumerable<Core.Domain.ColumbariumCentre> GetBySite(int sitId)
        {
            return _unitOfWork.ColumbariumCentres.GetBySite(sitId);
        }

        public int Add(Core.Domain.ColumbariumCentre columbariumCentre)
        {
            _unitOfWork.ColumbariumCentres.Add(columbariumCentre);

            _unitOfWork.Complete();

            return columbariumCentre.Id;
        }

        public bool Change(int id, Core.Domain.ColumbariumCentre columbariumCentre)
        {
            var columbariumCentreInDB = _unitOfWork.ColumbariumCentres.Get(id);

            if (columbariumCentreInDB.SiteId != columbariumCentre.SiteId
                && _unitOfWork.ColumbariumTransactions.Find(qt => qt.ColumbariumItem.ColumbariumCentreId == columbariumCentreInDB.Id).Any())
            {
                return false;
            }

            columbariumCentreInDB.Name = columbariumCentre.Name;
            columbariumCentreInDB.Description = columbariumCentre.Description;
            _unitOfWork.Complete();

            return true;
        }

        public bool Remove(int id)
        {
            if (_unitOfWork.ColumbariumTransactions.Find(qt => qt.ColumbariumItem.ColumbariumCentreId == id).Any())
            {
                return false;
            }

            var centreInDb = _unitOfWork.ColumbariumCentres.Get(id);

            if(centreInDb == null)
            {
                return false;
            }

            _unitOfWork.ColumbariumCentres.Remove(centreInDb);

            _unitOfWork.Complete();

            return true;
        }
    }
}