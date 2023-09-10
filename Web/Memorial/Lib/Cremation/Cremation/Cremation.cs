using System.Collections.Generic;
using System.Linq;
using Memorial.Core;
using AutoMapper;

namespace Memorial.Lib.Cremation
{
    public class Cremation : ICremation
    {
        private readonly IUnitOfWork _unitOfWork;

        public Cremation(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Core.Domain.Cremation GetById(int id)
        {
            return _unitOfWork.Cremations.GetActive(id);
        }

        public IEnumerable<Core.Domain.Cremation> GetBySite(int siteId)
        {
            return _unitOfWork.Cremations.GetBySite(siteId);
        }

        public int Add(Core.Domain.Cremation cremation)
        {
            _unitOfWork.Cremations.Add(cremation);
            _unitOfWork.Complete();

            return cremation.Id;
        }

        public bool Change(int id, Core.Domain.Cremation cremation)
        {
            var cremationInDB = GetById(id);

            if (cremationInDB.SiteId != cremation.SiteId
                && _unitOfWork.CremationTransactions.Find(ct => ct.CremationItem.Cremation.SiteId == cremationInDB.SiteId).Any())
            {
                return false;
            }

            cremationInDB.Name = cremation.Name;
            cremationInDB.Description = cremation.Description;
            _unitOfWork.Complete();

            return true;
        }

        public bool Remove(int id)
        {
            if (_unitOfWork.CremationTransactions.Find(ct => ct.CremationItem.Cremation.Id == id).Any())
            {
                return false;
            }

            var cremationInDB = GetById(id);

            if (cremationInDB == null)
            {
                return false;
            }

            _unitOfWork.Cremations.Remove(cremationInDB);
            _unitOfWork.Complete();

            return true;
        }
    }
}