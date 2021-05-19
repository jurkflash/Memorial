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
        private Core.Domain.QuadrangleCentre _centre;

        public Centre(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void SetCentre(int id)
        {
            _centre = _unitOfWork.QuadrangleCentres.GetActive(id);
        }

        public void SetCentre(Core.Domain.QuadrangleCentre quadrangleCentre)
        {
            _centre = quadrangleCentre;
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

        public Core.Domain.QuadrangleCentre GetCentre()
        {
            return _centre;
        }

        public Core.Domain.QuadrangleCentre GetCentre(int id)
        {
            return _unitOfWork.QuadrangleCentres.GetActive(id);
        }

        public QuadrangleCentreDto GetCentreDto(int id)
        {
            return Mapper.Map<Core.Domain.QuadrangleCentre, QuadrangleCentreDto>(GetCentre(id));
        }

        public IEnumerable<QuadrangleCentreDto> GetCentreDtos()
        {
            return Mapper.Map<IEnumerable<Core.Domain.QuadrangleCentre>, IEnumerable<QuadrangleCentreDto>>(_unitOfWork.QuadrangleCentres.GetAllActive());
        }

        public IEnumerable<Core.Domain.QuadrangleCentre> GetCentreBySite(byte sitId)
        {
            return _unitOfWork.QuadrangleCentres.GetBySite(sitId);
        }

        public IEnumerable<QuadrangleCentreDto> GetCentreDtosBySite(byte siteId)
        {
            return Mapper.Map<IEnumerable<Core.Domain.QuadrangleCentre>, IEnumerable<QuadrangleCentreDto>>(GetCentreBySite(siteId));
        }

        public bool Create(QuadrangleCentreDto quadrangleCentreDto)
        {
            _centre = new Core.Domain.QuadrangleCentre();
            Mapper.Map(quadrangleCentreDto, _centre);

            _centre.CreateDate = DateTime.Now;

            _unitOfWork.QuadrangleCentres.Add(_centre);

            return true;
        }

        public bool Update(Core.Domain.QuadrangleCentre quadrangleCentre)
        {
            quadrangleCentre.ModifyDate = DateTime.Now;

            return true;
        }

        public bool Delete(int id)
        {
            SetCentre(id);

            _centre.DeleteDate = DateTime.Now;

            return true;
        }
    }
}