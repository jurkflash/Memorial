using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Memorial.Core;
using Memorial.Core.Repositories;
using Memorial.Core.Dtos;
using AutoMapper;

namespace Memorial.Lib.CemeteryLandscapeCompany
{
    public class CemeteryLandscapeCompany : ICemeteryLandscapeCompany
    {
        private readonly IUnitOfWork _unitOfWork;
        private Core.Domain.CemeteryLandscapeCompany _cemeteryLandscapeCompany;
        public CemeteryLandscapeCompany(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void SetCemeteryLandscapeCompany(int id)
        {
            _cemeteryLandscapeCompany = _unitOfWork.CemeteryLandscapeCompanies.GetActive(id);
        }

        public Core.Domain.CemeteryLandscapeCompany GetCemeteryLandscapeCompany()
        {
            return _cemeteryLandscapeCompany;
        }

        public Core.Domain.CemeteryLandscapeCompany GetCemeteryLandscapeCompany(int id)
        {
            return _unitOfWork.CemeteryLandscapeCompanies.GetActive(id);
        }

        public CemeteryLandscapeCompanyDto GetCemeteryLandscapeCompanyDto(int id)
        {
            return Mapper.Map<Core.Domain.CemeteryLandscapeCompany, CemeteryLandscapeCompanyDto>(GetCemeteryLandscapeCompany(id));
        }

        public IEnumerable<CemeteryLandscapeCompanyDto> GetCemeteryLandscapeCompanyDtos()
        {
            return Mapper.Map<IEnumerable<Core.Domain.CemeteryLandscapeCompany>, IEnumerable<CemeteryLandscapeCompanyDto>>(_unitOfWork.CemeteryLandscapeCompanies.GetAllActive());
        }

        public int Create(CemeteryLandscapeCompanyDto cemeteryLandscapeCompanyDto)
        {
            _cemeteryLandscapeCompany = new Core.Domain.CemeteryLandscapeCompany();
            Mapper.Map(cemeteryLandscapeCompanyDto, _cemeteryLandscapeCompany);

            _cemeteryLandscapeCompany.CreatedDate = DateTime.Now;

            _unitOfWork.CemeteryLandscapeCompanies.Add(_cemeteryLandscapeCompany);

            _unitOfWork.Complete();

            return _cemeteryLandscapeCompany.Id;
        }

        public bool Update(CemeteryLandscapeCompanyDto cemeteryLandscapeCompanyDto)
        {
            var cemeteryLandscapeCompanyInDB = GetCemeteryLandscapeCompany(cemeteryLandscapeCompanyDto.Id);

            Mapper.Map(cemeteryLandscapeCompanyDto, cemeteryLandscapeCompanyInDB);

            cemeteryLandscapeCompanyInDB.ModifiedDate = DateTime.Now;

            _unitOfWork.Complete();

            return true;
        }

        public bool Delete(int id)
        {
            if (_unitOfWork.MiscellaneousTransactions.Find(mt => mt.CemeteryLandscapeCompanyId == id && mt.DeleteDate == null).Any())
            {
                return false;
            }

            SetCemeteryLandscapeCompany(id);

            _cemeteryLandscapeCompany.DeletedDate = DateTime.Now;

            _unitOfWork.Complete();

            return true;
        }
    }
}