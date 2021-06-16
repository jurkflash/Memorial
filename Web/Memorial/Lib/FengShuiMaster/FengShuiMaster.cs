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

        public int Create(FengShuiMasterDto fengShuiMasterDto)
        {
            _fengShuiMaster = new Core.Domain.FengShuiMaster();
            Mapper.Map(fengShuiMasterDto, _fengShuiMaster);

            _fengShuiMaster.CreateDate = DateTime.Now;

            _unitOfWork.FengShuiMasters.Add(_fengShuiMaster);

            _unitOfWork.Complete();

            return _fengShuiMaster.Id;
        }

        public bool Update(FengShuiMasterDto fengShuiMasterDto)
        {
            var fengShuiMasterInDB = GetFengShuiMaster(fengShuiMasterDto.Id);

            Mapper.Map(fengShuiMasterDto, fengShuiMasterInDB);

            fengShuiMasterInDB.ModifyDate = DateTime.Now;

            _unitOfWork.Complete();

            return true;
        }

        public bool Delete(int id)
        {
            if (_unitOfWork.CemeteryTransactions.Find(at => at.FengShuiMasterId == id && at.DeleteDate == null).Any())
            {
                return false;
            }

            SetFengShuiMaster(id);

            _fengShuiMaster.DeleteDate = DateTime.Now;

            _unitOfWork.Complete();

            return true;
        }
    }
}