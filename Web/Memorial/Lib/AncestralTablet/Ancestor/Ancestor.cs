using System;
using System.Collections.Generic;
using Memorial.Core;
using Memorial.Core.Dtos;
using AutoMapper;

namespace Memorial.Lib.Ancestor
{
    public class Ancestor : IAncestor
    {
        private readonly IUnitOfWork _unitOfWork;
        private Core.Domain.Ancestor _ancestor;

        public Ancestor(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void SetAncestor(int id)
        {
            _ancestor = _unitOfWork.Ancestors.GetActive(id);
        }

        public Core.Domain.Ancestor GetAncestor()
        {
            return _ancestor;
        }

        public AncestorDto GetAncestorDto()
        {
            return Mapper.Map<Core.Domain.Ancestor, AncestorDto>(GetAncestor());
        }

        public Core.Domain.Ancestor GetAncestor(int id)
        {
            return _unitOfWork.Ancestors.GetActive(id);
        }

        public AncestorDto GetAncestorDto(int id)
        {
            return Mapper.Map<Core.Domain.Ancestor, AncestorDto>(GetAncestor(id));
        }

        public IEnumerable<Core.Domain.Ancestor> GetAncestorsByAreaId(int id)
        {
            return _unitOfWork.Ancestors.GetByArea(id);
        }

        public IEnumerable<AncestorDto> GetAncestorDtosByAreaId(int id)
        {
            return Mapper.Map<IEnumerable<Core.Domain.Ancestor>, IEnumerable<AncestorDto>>(GetAncestorsByAreaId(id));
        }

        public IEnumerable<Core.Domain.Ancestor> GetAvailableAncestorsByAreaId(int id)
        {
            return _unitOfWork.Ancestors.GetAvailableByArea(id);
        }

        public IEnumerable<AncestorDto> GetAvailableAncestorDtosByAreaId(int id)
        {
            return Mapper.Map<IEnumerable<Core.Domain.Ancestor>, IEnumerable<AncestorDto>>(GetAvailableAncestorsByAreaId(id));
        }

        public string GetName()
        {
            return _ancestor.Name;
        }

        public float GetPrice()
        {
            return _ancestor.Price;
        }

        public float GetMaintenance()
        {
            return _ancestor.Maintenance;
        }

        public bool HasDeceased()
        {
            return _ancestor.hasDeceased;
        }

        public void SetHasDeceased(bool flag)
        {
            _ancestor.hasDeceased = flag;
        }

        public bool HasApplicant()
        {
            return _ancestor.ApplicantId == null ? false : true;
        }

        public bool HasFreeOrder()
        {
            return _ancestor.hasFreeOrder;
        }

        public int? GetApplicantId()
        {
            return _ancestor.ApplicantId;
        }

        public void SetApplicant(int applicantId)
        {
            _ancestor.ApplicantId = applicantId;
        }

        public void RemoveApplicant()
        {
            _ancestor.Applicant = null;
            _ancestor.ApplicantId = null;
        }

        public int GetAreaId()
        {
            return _ancestor.AncestralTabletAreaId;
        }

        public IDictionary<byte, IEnumerable<byte>> GetPositionsByAreaId(int areaId)
        {
            return _unitOfWork.Ancestors.GetPositionsByArea(areaId);
        }

        public bool Create(AncestorDto ancestorDto)
        {
            _ancestor = new Core.Domain.Ancestor();
            Mapper.Map(ancestorDto, _ancestor);

            _ancestor.CreateDate = DateTime.Now;

            _unitOfWork.Ancestors.Add(_ancestor);

            return true;
        }

        public bool Update(Core.Domain.Ancestor ancestor)
        {
            ancestor.ModifyDate = DateTime.Now;

            return true;
        }

        public bool Delete(int id)
        {
            SetAncestor(id);

            _ancestor.DeleteDate = DateTime.Now;

            return true;
        }


    }
}