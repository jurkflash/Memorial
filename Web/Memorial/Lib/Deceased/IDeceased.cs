using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Memorial.Core.Dtos;

namespace Memorial.Lib
{
    public interface IDeceased
    {
        void SetById(int id);

        void SetByIC(string IC);

        Core.Domain.Deceased GetDeceased();

        IEnumerable<Core.Domain.Deceased> GetByApplicant(int applicantId);

        IEnumerable<Core.Domain.Deceased> GetByQuadrangle(int quadrangleId);

        Core.Domain.Quadrangle GetQuadrangle();

        bool SetQuadrangle(int quadrangleId);

        bool RemoveQuadrangle();

        IEnumerable<DeceasedBriefDto> BriefDtosGetByApplicant(int applicantId);
    }
}