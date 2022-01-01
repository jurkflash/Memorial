using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Memorial.Core;
using Memorial.Core.Repositories;
using Memorial.Core.Dtos;
using AutoMapper;

namespace Memorial.Lib.Cremation
{
    public class Cremation : ICremation
    {
        private readonly IUnitOfWork _unitOfWork;
        private Core.Domain.Cremation _cremation;

        public Cremation(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void SetCremation(int id)
        {
            _cremation = _unitOfWork.Cremations.GetActive(id);
        }

        public Core.Domain.Cremation GetCremation()
        {
            return _cremation;
        }

        public CremationDto GetCremationDto()
        {
            return Mapper.Map<Core.Domain.Cremation, CremationDto>(GetCremation());
        }

        public Core.Domain.Cremation GetCremation(int id)
        {
            return _unitOfWork.Cremations.GetActive(id);
        }

        public CremationDto GetCremationDto(int id)
        {
            return Mapper.Map<Core.Domain.Cremation, CremationDto>(GetCremation(id));
        }

        public IEnumerable<CremationDto> GetCremationDtos()
        {
            return Mapper.Map<IEnumerable<Core.Domain.Cremation>, IEnumerable<CremationDto>>(_unitOfWork.Cremations.GetAllActive());
        }

        public IEnumerable<Core.Domain.Cremation> GetCremationBySite(int siteId)
        {
            return _unitOfWork.Cremations.GetBySite(siteId);
        }

        public IEnumerable<CremationDto> GetCremationDtosBySite(int siteId)
        {
            return Mapper.Map<IEnumerable<Core.Domain.Cremation>, IEnumerable<CremationDto>>(_unitOfWork.Cremations.GetBySite(siteId));
        }

        public string GetName()
        {
            return _cremation.Name;
        }

        public string GetDescription()
        {
            return _cremation.Description;
        }

        public int Create(CremationDto cremationDto)
        {
            _cremation = new Core.Domain.Cremation();
            Mapper.Map(cremationDto, _cremation);

            _unitOfWork.Cremations.Add(_cremation);

            _unitOfWork.Complete();

            return _cremation.Id;
        }

        public bool Update(CremationDto cremationDto)
        {
            var cremationInDB = GetCremation(cremationDto.Id);

            if (cremationInDB.SiteId != cremationDto.SiteDtoId
                && _unitOfWork.CremationTransactions.Find(ct => ct.CremationItem.Cremation.SiteId == cremationInDB.SiteId && ct.DeleteDate == null).Any())
            {
                return false;
            }

            Mapper.Map(cremationDto, cremationInDB);

            _unitOfWork.Complete();

            return true;
        }

        public bool Delete(int id)
        {
            if (_unitOfWork.CremationTransactions.Find(ct => ct.CremationItem.Cremation.Id == id && ct.DeleteDate == null).Any())
            {
                return false;
            }

            SetCremation(id);

            if(_cremation == null)
            {
                return false;
            }

            _unitOfWork.Cremations.Remove(_cremation);

            _unitOfWork.Complete();

            return true;
        }


    }
}