using System.Collections.Generic;
using System.Linq;
using Memorial.Core;
using AutoMapper;

namespace Memorial.Lib.CemeteryLandscapeCompany
{
    public class CemeteryLandscapeCompany : ICemeteryLandscapeCompany
    {
        private readonly IUnitOfWork _unitOfWork;
        public CemeteryLandscapeCompany(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Core.Domain.CemeteryLandscapeCompany Get(int id)
        {
            return _unitOfWork.CemeteryLandscapeCompanies.GetActive(id);
        }

        public IEnumerable<Core.Domain.CemeteryLandscapeCompany> GetAll()
        {
            return _unitOfWork.CemeteryLandscapeCompanies.GetAllActive();
        }

        public int Add(Core.Domain.CemeteryLandscapeCompany cemeteryLandscapeCompany)
        {
            _unitOfWork.CemeteryLandscapeCompanies.Add(cemeteryLandscapeCompany);

            _unitOfWork.Complete();

            return cemeteryLandscapeCompany.Id;
        }

        public bool Change(int id, Core.Domain.CemeteryLandscapeCompany cemeteryLandscapeCompany)
        {
            var cemeteryLandscapeCompanyInDB = _unitOfWork.CemeteryLandscapeCompanies.GetActive(id);

            cemeteryLandscapeCompanyInDB.Name = cemeteryLandscapeCompany.Name;
            cemeteryLandscapeCompanyInDB.ContactPerson = cemeteryLandscapeCompany.ContactPerson;
            cemeteryLandscapeCompanyInDB.ContactNumber = cemeteryLandscapeCompany.ContactNumber;
            cemeteryLandscapeCompanyInDB.Remark = cemeteryLandscapeCompany.Remark;
            _unitOfWork.Complete();

            return true;
        }

        public bool Remove(int id)
        {
            if (_unitOfWork.MiscellaneousTransactions.Find(mt => mt.CemeteryLandscapeCompanyId == id).Any())
            {
                return false;
            }

            var cemeteryLandscapeCompanyInDB = _unitOfWork.CemeteryLandscapeCompanies.GetActive(id);
            if(cemeteryLandscapeCompanyInDB != null)
            {
                _unitOfWork.CemeteryLandscapeCompanies.Remove(cemeteryLandscapeCompanyInDB);
                _unitOfWork.Complete();
            }

            return true;
        }
    }
}