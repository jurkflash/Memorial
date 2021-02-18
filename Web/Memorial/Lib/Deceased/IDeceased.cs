using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Memorial.Core.Dtos;

namespace Memorial.Lib
{
    public interface IDeceased
    {
        IEnumerable<DeceasedBriefDto> BriefDtosGetByApplicant(int applicantId);
    }
}