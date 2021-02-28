using Memorial.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Memorial.Lib.Miscellaneous;

namespace Memorial.Lib.Invoice
{
    public interface IMiscellaneous : IInvoice
    {
        bool Create(string AF, float amount, string remark);
    }
}