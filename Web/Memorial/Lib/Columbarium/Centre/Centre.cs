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

        public void SetCentre(Core.Domain.ColumbariumCentre quadrangleCentre)
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

        public Core.Domain.ColumbariumCentre GetCentre()
        {
            return _centre;
        }

        public Core.Domain.ColumbariumCentre GetCentre(int id)
        {
            return _unitOfWork.ColumbariumCentres.GetActive(id);
        }

        public ColumbariumCentreDto GetCentreDto(int id)
        {
            return Mapper.Map<Core.Domain.ColumbariumCentre, ColumbariumCentreDto>(GetCentre(id));
        }

        public IEnumerable<ColumbariumCentreDto> GetCentreDtos()
        {
            return Mapper.Map<IEnumerable<Core.Domain.ColumbariumCentre>, IEnumerable<ColumbariumCentreDto>>(_unitOfWork.ColumbariumCentres.GetAllActive());
        }

        public IEnumerable<Core.Domain.ColumbariumCentre> GetCentreBySite(byte sitId)
        {
            return _unitOfWork.ColumbariumCentres.GetBySite(sitId);
        }

        public IEnumerable<ColumbariumCentreDto> GetCentreDtosBySite(byte siteId)
        {
            return Mapper.Map<IEnumerable<Core.Domain.ColumbariumCentre>, IEnumerable<ColumbariumCentreDto>>(GetCentreBySite(siteId));
        }

        public bool Create(ColumbariumCentreDto quadrangleCentreDto)
        {
            _centre = new Core.Domain.ColumbariumCentre();
            Mapper.Map(quadrangleCentreDto, _centre);

            _centre.CreateDate = DateTime.Now;

            _unitOfWork.ColumbariumCentres.Add(_centre);

            return true;
        }

        public bool Update(Core.Domain.ColumbariumCentre quadrangleCentre)
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