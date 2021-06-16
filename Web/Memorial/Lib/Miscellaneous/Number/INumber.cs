﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Memorial.Core.Dtos;
using Memorial.Core.Repositories;

namespace Memorial.Lib.Miscellaneous
{
    public interface INumber
    {
        string GetNewAF(int miscellaneousItemId, int year);

        string GetNewIV(int miscellaneousItemId, int year);

        string GetNewRE(int miscellaneousItemId, int year);
    }
}