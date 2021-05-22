﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Memorial.Core.Dtos;
using Memorial.Core.Repositories;

namespace Memorial.Lib.Ancestor
{
    public interface INumber
    {
        string GetNewAF(int ancestralTabletItemId, int year);

        string GetNewIV(int ancestralTabletItemId, int year);
    }
}