using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Memorial.Core.Dtos;

namespace Memorial.Lib
{
    public interface IDeceased
    {
        Core.Domain.Deceased GetByIC(string IC);

        Core.Domain.Deceased GetActive(int id);

        Core.Domain.Quadrangle GetQuadrangle();

        bool SetQuadrangle(int quadrangleId);

        IEnumerable<DeceasedBriefDto> BriefDtosGetByApplicant(int applicantId);
    }
}