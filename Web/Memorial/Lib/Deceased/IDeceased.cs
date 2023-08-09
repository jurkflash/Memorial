using System;
using System.Collections.Generic;
using Memorial.Core.Dtos;

namespace Memorial.Lib.Deceased
{
    public interface IDeceased
    {
        IEnumerable<Core.Domain.Deceased> GetByApplicantId(int applicantId);







        void SetDeceased(int id);

        Core.Domain.Deceased GetDeceasedByIC(string ic);

        Core.Domain.Deceased GetDeceased();

        DeceasedDto GetDeceasedDto();

        Core.Domain.Deceased GetDeceased(int id);

        DeceasedDto GetDeceasedDto(int id);

        bool GetExistsByIC(string ic, int? excludeId = null);

        IEnumerable<Core.Domain.Deceased> GetDeceasedsByApplicantId(int applicantId);

        IEnumerable<DeceasedDto> GetDeceasedDtosByApplicantId(int applicantId);

        IEnumerable<DeceasedBriefDto> GetDeceasedBriefDtosByApplicantId(int applicantId);

        IEnumerable<Core.Domain.Deceased> GetDeceasedsExcludeFilter(int applicantId, string deceasedName);

        IEnumerable<DeceasedDto> GetDeceasedDtosExcludeFilter(int applicantId, string deceasedName);

        IEnumerable<Core.Domain.Deceased> GetDeceasedsByNicheId(int nicheId);

        IEnumerable<Core.Domain.Deceased> GetDeceasedsByAncestralTabletId(int ancestralTabletId);

        IEnumerable<Core.Domain.Deceased> GetDeceasedsByPlotId(int plotId);

        bool IsRecordLinked(int id);

        int Add(DeceasedDto deceasedDto);

        bool Update(DeceasedDto deceasedDto);

        bool Remove(int id);

        Core.Domain.Niche GetNiche();

        bool SetNiche(int nicheId);

        bool RemoveNiche();

        bool RemoveAncestralTablet();

        Core.Domain.AncestralTablet GetAncestralTablet();

        bool SetAncestralTablet(int ancestralTabletId);

        Core.Domain.Plot GetPlot();

        bool SetPlot(int plotId);

        bool RemovePlot();

        bool InstallNicheDeceased(int nicheId);

    }
}