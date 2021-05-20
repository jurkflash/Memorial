using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Memorial.Core.Dtos;
using Memorial.Core.Repositories;

namespace Memorial.Lib.Cemetery
{
    public interface INumber
    {
        string GetNewAF(int plotItemId, int year);

        string GetNewIV(int plotItemId, int year);
    }
}