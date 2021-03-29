using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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

        IEnumerable<Core.Domain.Deceased> GetDeceasedsExcludeFilter(int applicantId, string deceasedName);

        IEnumerable<Core.Domain.Deceased> GetDeceasedsByQuadrangleId(int quadrangleId);

        IEnumerable<Core.Domain.Deceased> GetDeceasedsByAncestorId(int ancestorId);

        int Create(DeceasedDto deceasedDto);

        bool Update(DeceasedDto deceasedDto);

        Core.Domain.Quadrangle GetQuadrangle();

        bool SetQuadrangle(int quadrangleId);

        bool RemoveQuadrangle();

        Core.Domain.Ancestor GetAncestor();

        bool SetAncestor(int ancestorId);

        bool RemoveAncestor();

        IEnumerable<DeceasedBriefDto> GetDeceasedBriefDtosByApplicantId(int applicantId);
    }
}