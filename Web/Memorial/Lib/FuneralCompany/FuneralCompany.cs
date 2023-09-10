using System.Collections.Generic;
using System.Linq;
using Memorial.Core;
using AutoMapper;

namespace Memorial.Lib.FuneralCompany
{
    public class FuneralCompany : IFuneralCompany
    {
        private readonly IUnitOfWork _unitOfWork;

        public FuneralCompany(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Core.Domain.FuneralCompany Get(int id)
        {
            return _unitOfWork.FuneralCompanies.Get(id);
        }

        public IEnumerable<Core.Domain.FuneralCompany> GetAll()
        {
            return _unitOfWork.FuneralCompanies.GetAllActive();
        }

        public int Add(Core.Domain.FuneralCompany funeralCompany)
        {
            _unitOfWork.FuneralCompanies.Add(funeralCompany);

            _unitOfWork.Complete();

            return funeralCompany.Id;
        }

        public bool Change(int id, Core.Domain.FuneralCompany funeralCompany)
        {
            var funeralCompanyInDb = _unitOfWork.FuneralCompanies.Get(id);

            funeralCompanyInDb.Name = funeralCompany.Name;
            funeralCompanyInDb.ContactPerson = funeralCompany.ContactPerson;
            funeralCompanyInDb.ContactNumber = funeralCompany.ContactNumber;
            funeralCompanyInDb.Remark = funeralCompany.Remark;
            _unitOfWork.Complete();

            return true;
        }

        public bool Remove(int id)
        {
            if (
                _unitOfWork.CremationTransactions.Find(at => at.FuneralCompanyId == id).Any() ||
                _unitOfWork.ColumbariumTransactions.Find(at => at.FuneralCompanyId == id).Any() ||
                _unitOfWork.SpaceTransactions.Find(at => at.FuneralCompanyId == id).Any())
            {
                return false;
            }

            var funeralCompanyInDb = _unitOfWork.FuneralCompanies.Get(id);

            if (funeralCompanyInDb != null)
            {
                _unitOfWork.FuneralCompanies.Remove(funeralCompanyInDb);
                _unitOfWork.Complete();
            }

            return true;
        }
    }
}