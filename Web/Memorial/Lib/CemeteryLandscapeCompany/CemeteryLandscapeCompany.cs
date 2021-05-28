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

        public bool CreateCemeteryLandscapeCompany(CemeteryLandscapeCompanyDto CemeteryLandscapeCompanyDto)
        {
            _cemeteryLandscapeCompany = new Core.Domain.CemeteryLandscapeCompany();
            Mapper.Map(CemeteryLandscapeCompanyDto, _cemeteryLandscapeCompany);

            _cemeteryLandscapeCompany.CreateDate = DateTime.Now;

            _unitOfWork.CemeteryLandscapeCompanies.Add(_cemeteryLandscapeCompany);

            _unitOfWork.Complete();

            return true;
        }

        public bool UpdateCemeteryLandscapeCompany(CemeteryLandscapeCompanyDto CemeteryLandscapeCompanyDto)
        {
            var CemeteryLandscapeCompanyInDB = GetCemeteryLandscapeCompany(CemeteryLandscapeCompanyDto.Id);

            Mapper.Map(CemeteryLandscapeCompanyDto, CemeteryLandscapeCompanyInDB);

            CemeteryLandscapeCompanyInDB.ModifyDate = DateTime.Now;

            _unitOfWork.Complete();

            return true;
        }

        public bool DeleteCemeteryLandscapeCompany(int id)
        {
            if (_unitOfWork.MiscellaneousTransactions.Find(mt => mt.CemeteryLandscapeCompanyId == id && mt.DeleteDate == null).Any())
            {
                return false;
            }

            SetCemeteryLandscapeCompany(id);

            _cemeteryLandscapeCompany.DeleteDate = DateTime.Now;

            _unitOfWork.Complete();

            return true;
        }
    }
}