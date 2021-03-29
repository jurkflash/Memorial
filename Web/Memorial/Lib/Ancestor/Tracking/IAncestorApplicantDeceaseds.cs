using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Memorial.Core.Dtos;

namespace Memorial.Lib.Ancestor
{
    public interface IAncestorApplicantDeceaseds
    {
        bool SetAncestorApplicantDeceaseds(int? applicantId = null, int? deceasedId = null);

        bool RollbackAncestorApplicantDeceaseds(string ancestorTransactionAF, int ancestorId);
    }
}