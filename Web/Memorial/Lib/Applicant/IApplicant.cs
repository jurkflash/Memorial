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

        ApplicantDto GetApplicantDto();

        Core.Domain.Applicant GetApplicant(int id);

        ApplicantDto GetApplicantDto(int id);

        IEnumerable<Core.Domain.Applicant> GetApplicants(string filter);

        IEnumerable<ApplicantDto> GetApplicantDtos(string filter);

        bool Create(Core.Domain.Applicant applicant);

        bool Update(Core.Domain.Applicant applicant);
    }
}