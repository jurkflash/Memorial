using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Memorial.Core;
using Memorial.Core.Repositories;
using Memorial.Core.Dtos;
using AutoMapper;

namespace Memorial.Lib.ApplicantDeceased
{
    public class ApplicantDeceased : IApplicantDeceased
    {
        private readonly IUnitOfWork _unitOfWork;

        private Core.Domain.ApplicantDeceased _applicantDeceased;

        public ApplicantDeceased(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void SetApplicantDeceased(int id)
        {
            _applicantDeceased = _unitOfWork.ApplicantDeceaseds.GetActive(id);
        }

        public void SetApplicantDeceased(int applicantId, int deceasedId)
        {
            _applicantDeceased = _unitOfWork.ApplicantDeceaseds.GetByApplicantDeceasedId(applicantId, deceasedId);
        }

        public Core.Domain.ApplicantDeceased GetApplicantDeceased()
        {
            return _applicantDeceased;
        }

        public ApplicantDeceasedDto GetApplicantDeceasedDto()
        {
            return Mapper.Map<Core.Domain.ApplicantDeceased, ApplicantDeceasedDto>(GetApplicantDeceased());
        }

        public Core.Domain.ApplicantDeceased GetApplicantDeceased(int applicantId, int deceasedId)
        {
            return _unitOfWork.ApplicantDeceaseds.GetByApplicantDeceasedId(applicantId, deceasedId);
        }

        public ApplicantDeceasedDto GetApplicantDeceasedDto(int applicantId, int deceasedId)
        {
            return Mapper.Map<Core.Domain.ApplicantDeceased, ApplicantDeceasedDto>(GetApplicantDeceased(applicantId, deceasedId));
        }

        public ApplicantDeceasedFlattenDto GetApplicantDeceasedFlattenDto(int applicantId, int deceasedId)
        {
            var ad = GetApplicantDeceasedDto(applicantId, deceasedId);

            return new Core.Dtos.ApplicantDeceasedFlattenDto()
            {
                ApplicantId = ad.ApplicantDtoId,
                ApplicantName = ad.ApplicantDto.Name,
                ApplicantName2 = ad.ApplicantDto.Name2,
                DeceasedId = ad.DeceasedDtoId,
                DeceasedName = ad.DeceasedDto.Name,
                DeceasedName2 = ad.DeceasedDto.Name2,
                Id = ad.Id,
                RelationshipTypeId = ad.RelationshipTypeDtoId,
                RelationshipTypeName = ad.RelationshipTypeDto.Name
            };
        }

        public byte GetRelationshipTypeId()
        {
            return _applicantDeceased.RelationshipTypeId;
        }

        public IEnumerable<Core.Domain.ApplicantDeceased> GetApplicantDeceasedsByApplicantId(int applicantId)
        {
            return _unitOfWork.ApplicantDeceaseds.GetByApplicantId(applicantId);
        }

        public IEnumerable<ApplicantDeceasedDto> GetApplicantDeceasedDtosByApplicantId(int applicantId)
        {
            return Mapper.Map<IEnumerable<Core.Domain.ApplicantDeceased>, IEnumerable<ApplicantDeceasedDto>>(GetApplicantDeceasedsByApplicantId(applicantId));
        }

        public IEnumerable<ApplicantDeceasedFlattenDto> GetApplicantDeceasedFlattenDtosByApplicantId(int applicantId)
        {
            foreach(var a in GetApplicantDeceasedsByApplicantId(applicantId))
            {
                yield return new Core.Dtos.ApplicantDeceasedFlattenDto()
                {
                    ApplicantId = a.ApplicantId,
                    ApplicantName = a.Applicant.Name,
                    ApplicantName2 = a.Applicant.Name2,
                    DeceasedId = a.DeceasedId,
                    DeceasedName = a.Deceased.Name,
                    DeceasedName2 = a.Deceased.Name2,
                    Id = a.Id,
                    RelationshipTypeId = a.RelationshipTypeId,
                    RelationshipTypeName = a.RelationshipType.Name
                };
            }
        }

        public IEnumerable<Core.Domain.ApplicantDeceased> GetApplicantDeceasedsByDeceasedId(int deceasedId)
        {
            return _unitOfWork.ApplicantDeceaseds.GetByDeceasedId(deceasedId);
        }

        public IEnumerable<ApplicantDeceasedDto> GetApplicantDeceasedDtosByDeceasedId(int deceasedId)
        {
            return Mapper.Map<IEnumerable<Core.Domain.ApplicantDeceased>, IEnumerable<ApplicantDeceasedDto>>(GetApplicantDeceasedsByDeceasedId(deceasedId));
        }

        public bool Create(int applicantId, int deceasedId, byte relationshipTypeId)
        {
            SetApplicantDeceased(applicantId, deceasedId);
            if (_applicantDeceased == null)
            {
                _applicantDeceased = new Core.Domain.ApplicantDeceased();
                _applicantDeceased.ApplicantId = applicantId;
                _applicantDeceased.DeceasedId = deceasedId;
                _applicantDeceased.RelationshipTypeId = relationshipTypeId;
                _applicantDeceased.CreatedDate = System.DateTime.Now;
                _unitOfWork.ApplicantDeceaseds.Add(_applicantDeceased);
                _unitOfWork.Complete();
            }
            else
                return false;

            return true;
        }

        public int CreateWithReturnId(int applicantId, int deceasedId, byte relationshipTypeId)
        {
            Create(applicantId, deceasedId, relationshipTypeId);

            return _applicantDeceased.Id;
        }

        public bool Update(int applicantId, int deceasedId, byte relationshipTypeId)
        {
            SetApplicantDeceased(applicantId, deceasedId);

            if(_applicantDeceased.RelationshipTypeId != relationshipTypeId)
            {
                _applicantDeceased.RelationshipTypeId = relationshipTypeId;

                _applicantDeceased.ModifiedDate = System.DateTime.Now;

                _unitOfWork.Complete();
            }

            return true;
        }

        public bool Delete()
        {
            _applicantDeceased.DeletedDate = System.DateTime.Now;

            _unitOfWork.Complete();

            return true;
        }

        public int DeleteWithReturnDeceasedId()
        {
            Delete();

            return _applicantDeceased.DeceasedId;
        }
    }
}