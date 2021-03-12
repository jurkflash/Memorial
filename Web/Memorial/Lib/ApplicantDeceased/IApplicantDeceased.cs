using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Memorial.Core.Dtos;

namespace Memorial.Lib.ApplicantDeceased
{
    public interface IApplicantDeceased
    {
        void SetApplicantDeceased(int id);

        void SetApplicantDeceased(int applicantId, int deceasedId);

        Core.Domain.ApplicantDeceased GetApplicantDeceased();

        ApplicantDeceasedDto GetApplicantDeceasedDto();

        Core.Domain.ApplicantDeceased GetApplicantDeceased(int applicantId, int deceasedId);

        ApplicantDeceasedDto GetApplicantDeceasedDto(int applicantId, int deceasedId);

        byte GetRelationshipTypeId();

        IEnumerable<Core.Domain.ApplicantDeceased> GetApplicantDeceasedsByApplicantId(int applicantId);

        IEnumerable<ApplicantDeceasedDto> GetApplicantDeceasedDtosByApplicantId(int applicantId);

        IEnumerable<ApplicantDeceasedFlattenDto> GetApplicantDeceasedFlattenDtosByApplicantId(int applicantId);

        IEnumerable<Core.Domain.ApplicantDeceased> GetApplicantDeceasedsByDeceasedId(int deceasedId);

        IEnumerable<ApplicantDeceasedDto> GetApplicantDeceasedDtosByDeceasedId(int deceasedId);

        bool Create(int applicantId, int deceasedId, byte relationshipTypeId);

        int CreateWithReturnId(int applicantId, int deceasedId, byte relationshipTypeId);

        bool Update(int applicantId, int deceasedId, byte relationshipTypeId);

        bool Delete();

        int DeleteWithReturnDeceasedId();

    }
}