using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Memorial.Core.Dtos;

namespace Memorial.Lib.FuneralCo
{
    public interface IFuneralCo
    {
        IEnumerable<FuneralCompanyDto> GetFuneralCompanyDtos();
    }
}