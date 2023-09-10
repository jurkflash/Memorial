using System.Collections.Generic;
using System.Linq;
using Memorial.Core;
using AutoMapper;

namespace Memorial.Lib.Miscellaneous
{
    public class Miscellaneous : IMiscellaneous
    {
        private readonly IUnitOfWork _unitOfWork;

        public Miscellaneous(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Core.Domain.Miscellaneous Get(int id)
        {
            return _unitOfWork.Miscellaneous.GetActive(id);
        }

        public IEnumerable<Core.Domain.Miscellaneous> GetBySite(int siteId)
        {
            return _unitOfWork.Miscellaneous.GetBySite(siteId);
        }

        public int Add(Core.Domain.Miscellaneous miscellaneous)
        {
            _unitOfWork.Miscellaneous.Add(miscellaneous);

            _unitOfWork.Complete();

            return miscellaneous.Id;
        }

        public bool Change(int miscellaneousId, Core.Domain.Miscellaneous miscellaneous)
        {
            var miscellaneousInDB = _unitOfWork.Miscellaneous.Get(miscellaneousId);

            if (miscellaneousInDB.SiteId != miscellaneous.SiteId
                && _unitOfWork.MiscellaneousTransactions.Find(ct => ct.MiscellaneousItem.Miscellaneous.SiteId == miscellaneous.SiteId).Any())
            {
                return false;
            }

            miscellaneousInDB.Name = miscellaneous.Name;
            miscellaneousInDB.Description = miscellaneous.Description;
            miscellaneousInDB.Remark = miscellaneous.Remark;
            _unitOfWork.Complete();

            return true;
        }

        public bool Remove(int id)
        {
            if (_unitOfWork.MiscellaneousTransactions.Find(ct => ct.MiscellaneousItem.Miscellaneous.Id == id).Any())
            {
                return false;
            }

            var miscellaneousInDB = _unitOfWork.Miscellaneous.Get(id);

            if (miscellaneousInDB == null)
            {
                return false;
            }

            _unitOfWork.Miscellaneous.Remove(miscellaneousInDB);

            _unitOfWork.Complete();

            return true;
        }
    }
}