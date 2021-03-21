using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Memorial.Core.Dtos;

namespace Memorial.Lib.Quadrangle
{
    public interface IQuadrangleApplicantDeceaseds
    {
        bool SetQuadrangleApplicantDeceaseds(int? applicantId = null, int? deceased1Id = null, int? deceased2Id = null);

        bool RollbackQuadrangleApplicantDeceaseds(string quadrangleTransactionAF, int quadrangleId);
    }
}