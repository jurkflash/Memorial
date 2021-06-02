using System;
using System.Collections.Generic;
using Memorial.Core.Dtos;

namespace Memorial.Lib.Deceased
{
    public interface IDeceased
    {
        void SetDeceased(int id);

        Core.Domain.Deceased GetDeceasedByIC(string ic);

        Core.Domain.Deceased GetDeceased();

        DeceasedDto GetDeceasedDto();

        Core.Domain.Deceased GetDeceased(int id);

        DeceasedDto GetDeceasedDto(int id);

        IEnumerable<Core.Domain.Deceased> GetDeceasedsByApplicantId(int applicantId);

        IEnumerable<DeceasedDto> GetDeceasedDtosByApplicantId(int applicantId);

        IEnumerable<DeceasedBriefDto> GetDeceasedBriefDtosByApplicantId(int applicantId);

        IEnumerable<Core.Domain.Deceased> GetDeceasedsExcludeFilter(int applicantId, string deceasedName);

        IEnumerable<Core.Domain.Deceased> GetDeceasedsByNicheId(int nicheId);

        IEnumerable<Core.Domain.Deceased> GetDeceasedsByAncestralTabletId(int ancestralTabletId);

        IEnumerable<Core.Domain.Deceased> GetDeceasedsByPlotId(int plotId);

        int Create(DeceasedDto deceasedDto);

        bool Update(DeceasedDto deceasedDto);

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