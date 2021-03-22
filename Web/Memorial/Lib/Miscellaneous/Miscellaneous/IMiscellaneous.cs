using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Memorial.Core.Dtos;

namespace Memorial.Lib.Miscellaneous
{
    public interface IMiscellaneous
    {
        void SetMiscellaneous(int id);

        Core.Domain.Miscellaneous GetMiscellaneous();

        MiscellaneousDto GetMiscellaneousDto();

        Core.Domain.Miscellaneous GetMiscellaneous(int id);

        MiscellaneousDto GetMiscellaneousDto(int id);

        IEnumerable<Core.Domain.Miscellaneous> GetMiscellaneousBySite(byte siteId);

        IEnumerable<MiscellaneousDto> GetMiscellaneousDtosBySite(byte siteId);

        string GetName();

        string GetDescription();

        string GetRemark();
    }
}