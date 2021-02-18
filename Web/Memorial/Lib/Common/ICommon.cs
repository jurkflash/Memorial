using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Memorial.Core.Domain;
using Memorial.Core.Dtos;

namespace Memorial.Lib
{
    public interface ICommon
    {
        float GetAmount(string AF, MasterCatalog masterCatalog);

        float GetNonOrderUnpaidAmount(string AF, MasterCatalog masterCatalog);

        bool DeleteForm(string AF, MasterCatalog masterCatalog);
    }
}