using Memorial.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Memorial.Lib.Ancestor;

namespace Memorial.Lib.Invoice
{
    public interface IAncestor : IInvoice
    {
        bool Create(string AF, float amount, string remark);
    }
}