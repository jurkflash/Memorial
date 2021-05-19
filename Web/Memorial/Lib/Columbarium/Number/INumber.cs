using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Memorial.Core.Dtos;
using Memorial.Core.Repositories;

namespace Memorial.Lib.Columbarium
{
    public interface INumber
    {
        string GetNewAF(int quadrangleItemId, int year);

        string GetNewIV(int quadrangleItemId, int year);
    }
}