using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Memorial.Core.Dtos;
using Memorial.Core.Repositories;

namespace Memorial.Lib.Quadrangle
{
    public interface INumber
    {
        string GetNewAF(int urnItemId, int year);

        string GetNewIV(int urnItemId, int year);
    }
}