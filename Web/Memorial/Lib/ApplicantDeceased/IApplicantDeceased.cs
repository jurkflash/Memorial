using System.Collections.Generic;
using Memorial.Core.Dtos;

namespace Memorial.Lib.ApplicantDeceased
{
    public interface IApplicantDeceased
    {
        Core.Domain.ApplicantDeceased GetByApplicantDeceasedId(int applicantId, int deceasedId);
        IEnumerable<Core.Domain.ApplicantDeceased> GetByApplicantId(int applicantId);
        int Add(int applicantId, int deceasedId, byte relationshipTypeId);
        bool Change(int applicantId, int deceasedId, byte relationshipTypeId);
        bool Remove(int id);
        Core.Domain.ApplicantDeceasedFlatten GetApplicantDeceasedFlatten(int applicantId, int deceasedId);
        IEnumerable<Core.Domain.ApplicantDeceasedFlatten> GetApplicantDeceasedFlattensByApplicantId(int applicantId);
    }
}