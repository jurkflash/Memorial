using System.Collections.Generic;
using Memorial.Core;

namespace Memorial.Lib.ApplicantDeceased
{
    public class ApplicantDeceased : IApplicantDeceased
    {
        private readonly IUnitOfWork _unitOfWork;

        public ApplicantDeceased(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Core.Domain.ApplicantDeceased GetByApplicantDeceasedId(int applicantId, int deceasedId)
        {
            return _unitOfWork.ApplicantDeceaseds.GetByApplicantDeceasedId(applicantId, deceasedId);
        }

        public IEnumerable<Core.Domain.ApplicantDeceased> GetByApplicantId(int applicantId)
        {
            return _unitOfWork.ApplicantDeceaseds.GetByApplicantId(applicantId);
        }

        public bool Remove(int id)
        {
            var applicantDeceasedInDb = _unitOfWork.ApplicantDeceaseds.Get(id);

            if(applicantDeceasedInDb != null)
            {
                _unitOfWork.ApplicantDeceaseds.Remove(applicantDeceasedInDb);
                _unitOfWork.Complete();
            }

            return true;
        }

        public bool Change(int applicantId, int deceasedId, byte relationshipTypeId)
        {
            var ad = _unitOfWork.ApplicantDeceaseds.GetByApplicantDeceasedId(applicantId, deceasedId);

            if (ad != null && ad.RelationshipTypeId != relationshipTypeId)
            {
                ad.RelationshipTypeId = relationshipTypeId;
                _unitOfWork.Complete();
            }

            return true;
        }

        public Core.Domain.ApplicantDeceasedFlatten GetApplicantDeceasedFlatten(int applicantId, int deceasedId)
        {
            var ad = _unitOfWork.ApplicantDeceaseds.GetByApplicantDeceasedId(applicantId, deceasedId);

            return new Core.Domain.ApplicantDeceasedFlatten()
            {
                ApplicantId = ad.ApplicantId,
                ApplicantName = ad.Applicant.Name,
                ApplicantName2 = ad.Applicant.Name2,
                DeceasedId = ad.DeceasedId,
                DeceasedName = ad.Deceased.Name,
                DeceasedName2 = ad.Deceased.Name2,
                Id = ad.Id,
                RelationshipTypeId = ad.RelationshipTypeId,
                RelationshipTypeName = ad.RelationshipType.Name
            };
        }

        public IEnumerable<Core.Domain.ApplicantDeceasedFlatten> GetApplicantDeceasedFlattensByApplicantId(int applicantId)
        {
            foreach(var a in GetByApplicantId(applicantId))
            {
                yield return new Core.Domain.ApplicantDeceasedFlatten()
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

        public int Add(int applicantId, int deceasedId, byte relationshipTypeId)
        {
            var ad = _unitOfWork.ApplicantDeceaseds.GetByApplicantDeceasedId(applicantId, deceasedId);
            if (ad != null)
                return ad.Id;

            var applicantDeceased = new Core.Domain.ApplicantDeceased();
            applicantDeceased.ApplicantId = applicantId;
            applicantDeceased.DeceasedId = deceasedId;
            applicantDeceased.RelationshipTypeId = relationshipTypeId;
            _unitOfWork.ApplicantDeceaseds.Add(applicantDeceased);
            _unitOfWork.Complete();
            return applicantDeceased.Id;
        }        
    }
}