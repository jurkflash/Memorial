using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Memorial.Core;
using Memorial.Core.Repositories;
using Memorial.Core.Dtos;
using AutoMapper;

namespace Memorial.Lib.FuneralCompany
{
    public class FuneralCompany : IFuneralCompany
    {
        private readonly IUnitOfWork _unitOfWork;
        private Core.Domain.FuneralCompany _funeralCompany;
        public FuneralCompany(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void SetFuneralCompany(int id)
        {
            _funeralCompany = _unitOfWork.FuneralCompanies.GetActive(id);
        }

        public Core.Domain.FuneralCompany GetFuneralCompany()
        {
            return _funeralCompany;
        }

        public Core.Domain.FuneralCompany GetFuneralCompany(int id)
        {
            return _unitOfWork.FuneralCompanies.GetActive(id);
        }

        public FuneralCompanyDto GetFuneralCompanyDto(int id)
        {
            return Mapper.Map<Core.Domain.FuneralCompany, FuneralCompanyDto>(GetFuneralCompany(id));
        }

        public IEnumerable<FuneralCompanyDto> GetFuneralCompanyDtos()
        {
            return Mapper.Map<IEnumerable<Core.Domain.FuneralCompany>, IEnumerable<FuneralCompanyDto>>(_unitOfWork.FuneralCompanies.GetAllActive());
        }

        public bool CreateFuneralCompany(FuneralCompanyDto funeralCompanyDto)
        {
            _funeralCompany = new Core.Domain.FuneralCompany();
            Mapper.Map(funeralCompanyDto, _funeralCompany);

            _funeralCompany.CreateDate = DateTime.Now;

            _unitOfWork.FuneralCompanies.Add(_funeralCompany);

            _unitOfWork.Complete();

            return true;
        }

        public bool UpdateFuneralCompany(FuneralCompanyDto funeralCompanyDto)
        {
            var funeralCompanyInDB = GetFuneralCompany(funeralCompanyDto.Id);

            Mapper.Map(funeralCompanyDto, funeralCompanyInDB);

            funeralCompanyInDB.ModifyDate = DateTime.Now;

            _unitOfWork.Complete();

            return true;
        }

        public bool DeleteFuneralCompany(int id)
        {
            if (
                _unitOfWork.CremationTransactions.Find(at => at.FuneralCompanyId == id && at.DeleteDate == null).Any() ||
                _unitOfWork.QuadrangleTransactions.Find(at => at.FuneralCompanyId == id && at.DeleteDate == null).Any() ||
                _unitOfWork.SpaceTransactions.Find(at => at.FuneralCompanyId == id && at.DeleteDate == null).Any())
            {
                return false;
            }

            SetFuneralCompany(id);

            _funeralCompany.DeleteDate = DateTime.Now;

            _unitOfWork.Complete();

            return true;
        }
    }
}