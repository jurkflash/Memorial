using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Memorial.Core;
using Memorial.Core.Repositories;
using Memorial.Core.Dtos;
using AutoMapper;

namespace Memorial.Lib.Miscellaneous
{
    public class Miscellaneous : IMiscellaneous
    {
        private readonly IUnitOfWork _unitOfWork;
        private Core.Domain.Miscellaneous _miscellaneous;

        public Miscellaneous(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void SetMiscellaneous(int id)
        {
            _miscellaneous = _unitOfWork.Miscellaneous.GetActive(id);
        }

        public Core.Domain.Miscellaneous GetMiscellaneous()
        {
            return _miscellaneous;
        }

        public MiscellaneousDto GetMiscellaneousDto()
        {
            return Mapper.Map<Core.Domain.Miscellaneous, MiscellaneousDto>(GetMiscellaneous());
        }

        public Core.Domain.Miscellaneous GetMiscellaneous(int id)
        {
            return _unitOfWork.Miscellaneous.GetActive(id);
        }

        public MiscellaneousDto GetMiscellaneousDto(int id)
        {
            return Mapper.Map<Core.Domain.Miscellaneous, MiscellaneousDto>(GetMiscellaneous(id));
        }

        public IEnumerable<MiscellaneousDto> GetMiscellaneousDtos()
        {
            return Mapper.Map<IEnumerable<Core.Domain.Miscellaneous>, IEnumerable<MiscellaneousDto>>(_unitOfWork.Miscellaneous.GetAllActive());
        }

        public IEnumerable<Core.Domain.Miscellaneous> GetMiscellaneousBySite(int siteId)
        {
            return _unitOfWork.Miscellaneous.GetBySite(siteId);
        }

        public IEnumerable<MiscellaneousDto> GetMiscellaneousDtosBySite(int siteId)
        {
            return Mapper.Map<IEnumerable<Core.Domain.Miscellaneous>, IEnumerable<MiscellaneousDto>>(_unitOfWork.Miscellaneous.GetBySite(siteId));
        }

        public string GetName()
        {
            return _miscellaneous.Name;
        }

        public string GetDescription()
        {
            return _miscellaneous.Description;
        }

        public string GetRemark()
        {
            return _miscellaneous.Remark;
        }

        public int Create(MiscellaneousDto miscellaneousDto)
        {
            _miscellaneous = new Core.Domain.Miscellaneous();
            Mapper.Map(miscellaneousDto, _miscellaneous);

            _miscellaneous.CreateDate = DateTime.Now;

            _unitOfWork.Miscellaneous.Add(_miscellaneous);

            _unitOfWork.Complete();

            return _miscellaneous.Id;
        }

        public bool Update(MiscellaneousDto miscellaneousDto)
        {
            var miscellaneousInDB = GetMiscellaneous(miscellaneousDto.Id);

            if (miscellaneousInDB.SiteId != miscellaneousDto.SiteDtoId
                && _unitOfWork.MiscellaneousTransactions.Find(ct => ct.MiscellaneousItem.Miscellaneous.SiteId == miscellaneousInDB.SiteId && ct.DeleteDate == null).Any())
            {
                return false;
            }

            Mapper.Map(miscellaneousDto, miscellaneousInDB);

            miscellaneousInDB.ModifyDate = DateTime.Now;

            _unitOfWork.Complete();

            return true;
        }

        public bool Delete(int id)
        {
            if (_unitOfWork.MiscellaneousTransactions.Find(ct => ct.MiscellaneousItem.Miscellaneous.Id == id && ct.DeleteDate == null).Any())
            {
                return false;
            }

            SetMiscellaneous(id);

            _miscellaneous.DeleteDate = DateTime.Now;

            _unitOfWork.Complete();

            return true;
        }

    }
}