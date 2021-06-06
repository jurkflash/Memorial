using System;
using System.Collections.Generic;
using Memorial.Core;
using Memorial.Core.Dtos;
using AutoMapper;

namespace Memorial.Lib.AncestralTablet
{
    public class AncestralTablet : IAncestralTablet
    {
        private readonly IUnitOfWork _unitOfWork;
        private Core.Domain.AncestralTablet _ancestralTablet;

        public AncestralTablet(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void SetAncestralTablet(int id)
        {
            _ancestralTablet = _unitOfWork.AncestralTablets.GetActive(id);
        }

        public Core.Domain.AncestralTablet GetAncestralTablet()
        {
            return _ancestralTablet;
        }

        public AncestralTabletDto GetAncestralTabletDto()
        {
            return Mapper.Map<Core.Domain.AncestralTablet, AncestralTabletDto>(GetAncestralTablet());
        }

        public Core.Domain.AncestralTablet GetAncestralTablet(int id)
        {
            return _unitOfWork.AncestralTablets.GetActive(id);
        }

        public AncestralTabletDto GetAncestralTabletDto(int id)
        {
            return Mapper.Map<Core.Domain.AncestralTablet, AncestralTabletDto>(GetAncestralTablet(id));
        }

        public IEnumerable<Core.Domain.AncestralTablet> GetAncestralTabletsByAreaId(int id)
        {
            return _unitOfWork.AncestralTablets.GetByArea(id);
        }

        public IEnumerable<AncestralTabletDto> GetAncestralTabletDtosByAreaId(int id)
        {
            return Mapper.Map<IEnumerable<Core.Domain.AncestralTablet>, IEnumerable<AncestralTabletDto>>(GetAncestralTabletsByAreaId(id));
        }

        public IEnumerable<Core.Domain.AncestralTablet> GetAvailableAncestralTabletsByAreaId(int id)
        {
            return _unitOfWork.AncestralTablets.GetAvailableByArea(id);
        }

        public IEnumerable<AncestralTabletDto> GetAvailableAncestralTabletDtosByAreaId(int id)
        {
            return Mapper.Map<IEnumerable<Core.Domain.AncestralTablet>, IEnumerable<AncestralTabletDto>>(GetAvailableAncestralTabletsByAreaId(id));
        }

        public string GetName()
        {
            return _ancestralTablet.Name;
        }

        public float GetPrice()
        {
            return _ancestralTablet.Price;
        }

        public float GetMaintenance()
        {
            return _ancestralTablet.Maintenance;
        }

        public bool HasDeceased()
        {
            return _ancestralTablet.hasDeceased;
        }

        public void SetHasDeceased(bool flag)
        {
            _ancestralTablet.hasDeceased = flag;
        }

        public bool HasApplicant()
        {
            return _ancestralTablet.ApplicantId == null ? false : true;
        }

        public bool HasFreeOrder()
        {
            return _ancestralTablet.hasFreeOrder;
        }

        public int? GetApplicantId()
        {
            return _ancestralTablet.ApplicantId;
        }

        public void SetApplicant(int applicantId)
        {
            _ancestralTablet.ApplicantId = applicantId;
        }

        public void RemoveApplicant()
        {
            _ancestralTablet.Applicant = null;
            _ancestralTablet.ApplicantId = null;
        }

        public int GetAreaId()
        {
            return _ancestralTablet.AncestralTabletAreaId;
        }

        public IDictionary<byte, IEnumerable<byte>> GetPositionsByAreaId(int areaId)
        {
            return _unitOfWork.AncestralTablets.GetPositionsByArea(areaId);
        }

        public Core.Domain.AncestralTablet Create(AncestralTabletDto ancestralTabletDto)
        {
            _ancestralTablet = new Core.Domain.AncestralTablet();
            Mapper.Map(ancestralTabletDto, _ancestralTablet);

            _ancestralTablet.CreateDate = DateTime.Now;

            _unitOfWork.AncestralTablets.Add(_ancestralTablet);

            return _ancestralTablet;
        }

        public bool Update(Core.Domain.AncestralTablet ancestralTablet)
        {
            ancestralTablet.ModifyDate = DateTime.Now;

            return true;
        }

        public bool Delete(int id)
        {
            SetAncestralTablet(id);

            _ancestralTablet.DeleteDate = DateTime.Now;

            return true;
        }


    }
}