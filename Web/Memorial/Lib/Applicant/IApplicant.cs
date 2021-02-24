using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Memorial.Core.Dtos;

namespace Memorial.Lib
{
    public interface IApplicant
    {
        void SetApplicant(int id);

        void SetById(int id);

        void SetByIC(string IC);

        Core.Domain.Applicant GetApplicant();

        ApplicantDto DtosGetApplicant();
    }
}