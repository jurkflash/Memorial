using System.Collections.Generic;
using System.Linq;
using Memorial.Core;
using AutoMapper;

namespace Memorial.Lib.Urn
{
    public class Urn : IUrn
    {
        private readonly IUnitOfWork _unitOfWork;

        public Urn(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Core.Domain.Urn Get(int id)
        {
            return _unitOfWork.Urns.GetActive(id);
        }

        public IEnumerable<Core.Domain.Urn> GetBySite(int siteId)
        {
            return _unitOfWork.Urns.GetBySite(siteId);
        }

        public int Add(Core.Domain.Urn urn)
        {
            _unitOfWork.Urns.Add(urn);

            _unitOfWork.Complete();

            return urn.Id;
        }

        public bool Change(int id, Core.Domain.Urn urn)
        {
            var urnInDB = _unitOfWork.Urns.Get(id);

            if (urnInDB.SiteId != urn.SiteId
                && _unitOfWork.UrnTransactions.Find(ct => ct.UrnItem.Urn.SiteId == urnInDB.SiteId).Any())
            {
                return false;
            }

            urnInDB.Name = urn.Name;
            urnInDB.Description = urn.Description;
            urnInDB.Remark = urn.Remark;
            _unitOfWork.Complete();

            return true;
        }

        public bool Remove(int id)
        {
            if (_unitOfWork.UrnTransactions.Find(ct => ct.UrnItem.Urn.Id == id).Any())
            {
                return false;
            }

            var urnInDB = _unitOfWork.Urns.Get(id);

            if (urnInDB == null)
            {
                return false;
            }

            _unitOfWork.Urns.Remove(urnInDB);

            _unitOfWork.Complete();

            return true;

        }
    }
}