using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Memorial.Core;
using Memorial.Core.Dtos;
using AutoMapper;

namespace Memorial.Lib.Columbarium
{
    public class Centre : ICentre
    {
        private readonly IUnitOfWork _unitOfWork;
        private Core.Domain.ColumbariumCentre _centre;

        public Centre(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void SetCentre(int id)
        {
            _centre = _unitOfWork.ColumbariumCentres.GetActive(id);
        }

        public void SetCentre(Core.Domain.ColumbariumCentre columbariumCentre)
        {
            _centre = columbariumCentre;
        }

        public int GetID()
        {
            return _centre.Id;
        }

        public string GetName()
        {
            return _centre.Name;
        }

        public string GetDescription()
        {
            return _centre.Description;
        }

        public Core.Domain.ColumbariumCentre GetCentre()
        {
            return _centre;
        }

        public Core.Domain.ColumbariumCentre GetCentre(int id)
        {
            return _unitOfWork.ColumbariumCentres.GetActive(id);
        }

        public ColumbariumCentreDto GetCentreDto()
        {
            return Mapper.Map<Core.Domain.ColumbariumCentre, ColumbariumCentreDto>(GetCentre());
        }

        public ColumbariumCentreDto GetCentreDto(int id)
        {
            return Mapper.Map<Core.Domain.ColumbariumCentre, ColumbariumCentreDto>(GetCentre(id));
        }

        public IEnumerable<ColumbariumCentreDto> GetCentreDtos()
        {
            return Mapper.Map<IEnumerable<Core.Domain.ColumbariumCentre>, IEnumerable<ColumbariumCentreDto>>(_unitOfWork.ColumbariumCentres.GetAllActive());
        }

        public IEnumerable<Core.Domain.ColumbariumCentre> GetCentreBySite(int sitId)
        {
            return _unitOfWork.ColumbariumCentres.GetBySite(sitId);
        }

        public IEnumerable<ColumbariumCentreDto> GetCentreDtosBySite(int siteId)
        {
            return Mapper.Map<IEnumerable<Core.Domain.ColumbariumCentre>, IEnumerable<ColumbariumCentreDto>>(GetCentreBySite(siteId));
        }

        public int Create(ColumbariumCentreDto columbariumCentreDto)
        {
            _centre = new Core.Domain.ColumbariumCentre();
            Mapper.Map(columbariumCentreDto, _centre);

            _unitOfWork.ColumbariumCentres.Add(_centre);

            _unitOfWork.Complete();

            return _centre.Id;
        }

        public bool Update(ColumbariumCentreDto columbariumCentreDto)
        {
            var columbariumCentreInDB = GetCentre(columbariumCentreDto.Id);

            if (columbariumCentreInDB.SiteId != columbariumCentreDto.SiteDtoId
                && _unitOfWork.ColumbariumTransactions.Find(qt => qt.ColumbariumItem.ColumbariumCentreId == columbariumCentreInDB.Id).Any())
            {
                return false;
            }

            Mapper.Map(columbariumCentreDto, columbariumCentreInDB);

            _unitOfWork.Complete();

            return true;
        }

        public bool Delete(int id)
        {
            if (_unitOfWork.ColumbariumTransactions.Find(qt => qt.ColumbariumItem.ColumbariumCentreId == id).Any())
            {
                return false;
            }

            SetCentre(id);

            if(_centre == null)
            {
                return false;
            }

            _unitOfWork.ColumbariumCentres.Remove(_centre);

            _unitOfWork.Complete();

            return true;
        }
    }
}