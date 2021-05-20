using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Memorial.Core.Dtos;

namespace Memorial.Lib.Cemetery
{
    public interface IPlotApplicantDeceaseds
    {
        bool SetPlotApplicantDeceaseds(int? applicantId = null, int? deceased1Id = null, int? deceased2Id = null);

        bool RollbackPlotApplicantDeceaseds(string plotTransactionAF, int plotId);
    }
}