using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Memorial.Core.Dtos;
using Memorial.Core.Repositories;

namespace Memorial.Lib
{
    public interface ICremationNumber
    {
        string GetNewAF(int cremationItemId, int year);

        string GetNewIV(int cremationItemId, int year);
    }
}