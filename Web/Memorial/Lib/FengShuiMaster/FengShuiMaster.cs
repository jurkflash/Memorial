using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Memorial.Core;
using Memorial.Core.Repositories;
using Memorial.Core.Dtos;
using AutoMapper;

namespace Memorial.Lib.FengShuiMaster
{
    public class FengShuiMaster
    {
        private readonly IUnitOfWork _unitOfWork;
        private Core.Domain.FengShuiMaster _fengShuiMaster;
        public FengShuiMaster(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void SetFuneralCompany(int id)
        {
            _fengShuiMaster = _unitOfWork.FengShuiMasters.GetActive(id);
        }

        public Core.Domain.FengShuiMaster GetFuneralCompany()
        {
            return _fengShuiMaster;
        }

        public Core.Domain.FengShuiMaster GetFuneralCompany(int id)
        {
            return _unitOfWork.FengShuiMasters.GetActive(id);
        }

        public FuneralCompanyDto GetFuneralCompanyDto(int id)
        {
            return Mapper.Map<Core.Domain.FengShuiMaster, FuneralCompanyDto>(GetFuneralCompany(id));
        }

        public IEnumerable<FuneralCompanyDto> GetFuneralCompanyDtos()
        {
            return Mapper.Map<IEnumerable<Core.Domain.FengShuiMaster>, IEnumerable<FuneralCompanyDto>>(_unitOfWork.FengShuiMasters.GetAllActive());
        }

        public bool CreateFuneralCompany(FuneralCompanyDto funeralCompanyDto)
        {
            _fengShuiMaster = new Core.Domain.FengShuiMaster();
            Mapper.Map(funeralCompanyDto, _fengShuiMaster);

            _fengShuiMaster.CreateDate = DateTime.Now;

            _unitOfWork.FengShuiMasters.Add(_fengShuiMaster);

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
            //if (
            //    _unitOfWork.CremationTransactions.Find(at => at.FuneralCompanyId == id && at.DeleteDate == null).Any() ||
            //    _unitOfWork.QuadrangleTransactions.Find(at => at.FuneralCompanyId == id && at.DeleteDate == null).Any() ||
            //    _unitOfWork.SpaceTransactions.Find(at => at.FuneralCompanyId == id && at.DeleteDate == null).Any())
            //{
            //    return false;
            //}

            SetFuneralCompany(id);

            _fengShuiMaster.DeleteDate = DateTime.Now;

            _unitOfWork.Complete();

            return true;
        }
    }
}