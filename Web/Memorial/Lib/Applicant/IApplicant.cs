using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Memorial.Core.Dtos;

namespace Memorial.Lib.Applicant
{
    public interface IApplicant
    {
        void SetApplicant(int id);

        Core.Domain.Applicant GetApplicantByIC(string ic);

        Core.Domain.Applicant GetApplicant();

        bool GetExistsByIC(string ic, int? excludeId = null);

        ApplicantDto GetApplicantDto();

        Core.Domain.Applicant GetApplicant(int id);

        ApplicantDto GetApplicantDto(int id);

        IEnumerable<Core.Domain.Applicant> GetApplicants(string filter);

        IEnumerable<ApplicantDto> GetApplicantDtos(string filter);

        bool Create(ApplicantDto applicantDto);

        bool Update(ApplicantDto applicantDto);

        bool Remove(int id);

        bool IsRecordLinked(int id);
    }
}