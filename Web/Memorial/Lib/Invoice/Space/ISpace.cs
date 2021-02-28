﻿using Memorial.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Memorial.Lib.Space;

namespace Memorial.Lib.Invoice
{
    public interface ISpace : IInvoice
    {
        bool Create(string AF, float amount, string remark);
    }
}