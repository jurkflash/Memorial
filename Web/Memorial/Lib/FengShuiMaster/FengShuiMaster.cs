using System.Collections.Generic;
using System.Linq;
using Memorial.Core;
using AutoMapper;

namespace Memorial.Lib.FengShuiMaster
{
    public class FengShuiMaster : IFengShuiMaster
    {
        private readonly IUnitOfWork _unitOfWork;
        public FengShuiMaster(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Core.Domain.FengShuiMaster Get(int id)
        {
            return _unitOfWork.FengShuiMasters.GetActive(id);
        }

        public IEnumerable<Core.Domain.FengShuiMaster> GetAll()
        {
            return _unitOfWork.FengShuiMasters.GetAll();
        }

        public int Add(Core.Domain.FengShuiMaster fengShuiMaster)
        {
            _unitOfWork.FengShuiMasters.Add(fengShuiMaster);

            _unitOfWork.Complete();

            return fengShuiMaster.Id;
        }

        public bool Change(int id, Core.Domain.FengShuiMaster fengShuiMaster)
        {
            var fengShuiMasterInDB = _unitOfWork.FengShuiMasters.Get(id);

            Mapper.Map(fengShuiMaster, fengShuiMasterInDB);

            _unitOfWork.Complete();

            return true;
        }

        public bool Remove(int id)
        {
            if (_unitOfWork.CemeteryTransactions.Find(at => at.FengShuiMasterId == id).Any())
            {
                return false;
            }

            var fengShuiMasterInDb = _unitOfWork.FengShuiMasters.Get(id);
            if(fengShuiMasterInDb != null)
            {
                _unitOfWork.FengShuiMasters.Remove(fengShuiMasterInDb);
                _unitOfWork.Complete();
            }

            return true;
        }        
    }
}