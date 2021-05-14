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
    public class FengShuiMaster : IFengShuiMaster
    {
        private readonly IUnitOfWork _unitOfWork;
        private Core.Domain.FengShuiMaster _fengShuiMaster;
        public FengShuiMaster(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void SetFengShuiMaster(int id)
        {
            _fengShuiMaster = _unitOfWork.FengShuiMasters.GetActive(id);
        }

        public Core.Domain.FengShuiMaster GetFengShuiMaster()
        {
            return _fengShuiMaster;
        }

        public Core.Domain.FengShuiMaster GetFengShuiMaster(int id)
        {
            return _unitOfWork.FengShuiMasters.GetActive(id);
        }

        public FengShuiMasterDto GetFengShuiMasterDto(int id)
        {
            return Mapper.Map<Core.Domain.FengShuiMaster, FengShuiMasterDto>(GetFengShuiMaster(id));
        }

        public IEnumerable<FengShuiMasterDto> GetFengShuiMasterDtos()
        {
            return Mapper.Map<IEnumerable<Core.Domain.FengShuiMaster>, IEnumerable<FengShuiMasterDto>>(_unitOfWork.FengShuiMasters.GetAllActive());
        }

        public bool CreateFengShuiMaster(FengShuiMasterDto FengShuiMasterDto)
        {
            _fengShuiMaster = new Core.Domain.FengShuiMaster();
            Mapper.Map(FengShuiMasterDto, _fengShuiMaster);

            _fengShuiMaster.CreateDate = DateTime.Now;

            _unitOfWork.FengShuiMasters.Add(_fengShuiMaster);

            _unitOfWork.Complete();

            return true;
        }

        public bool UpdateFengShuiMaster(FengShuiMasterDto FengShuiMasterDto)
        {
            var FengShuiMasterInDB = GetFengShuiMaster(FengShuiMasterDto.Id);

            Mapper.Map(FengShuiMasterDto, FengShuiMasterInDB);

            FengShuiMasterInDB.ModifyDate = DateTime.Now;

            _unitOfWork.Complete();

            return true;
        }

        public bool DeleteFengShuiMaster(int id)
        {
            //if (
            //    _unitOfWork.CremationTransactions.Find(at => at.FengShuiMasterId == id && at.DeleteDate == null).Any() ||
            //    _unitOfWork.QuadrangleTransactions.Find(at => at.FengShuiMasterId == id && at.DeleteDate == null).Any() ||
            //    _unitOfWork.SpaceTransactions.Find(at => at.FengShuiMasterId == id && at.DeleteDate == null).Any())
            //{
            //    return false;
            //}

            SetFengShuiMaster(id);

            _fengShuiMaster.DeleteDate = DateTime.Now;

            _unitOfWork.Complete();

            return true;
        }
    }
}