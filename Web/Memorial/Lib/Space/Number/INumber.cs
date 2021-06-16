using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Memorial.Core.Dtos;
using Memorial.Core.Repositories;

namespace Memorial.Lib.Space
{
    public interface INumber
    {
        string GetNewAF(int spaceItemId, int year);

        string GetNewIV(int spaceItemId, int year);

        string GetNewRE(int spaceItemId, int year);
    }
}