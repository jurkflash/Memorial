using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Memorial.Core.Dtos;
using Memorial.Core.Repositories;

namespace Memorial.Lib
{
    public interface IAncestorNumber
    {
        string GetNewAF(int ancestorItemId, int year);

        string GetNewIV(int ancestorItemId, int year);
    }
}