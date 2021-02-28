using Memorial.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Memorial.Lib.Receipt
{
    public interface ISpace : IReceipt
    {
        bool Create(string AF, string IV, float amount, string remark, byte paymentMethodId, string paymentRemark);
    }
}