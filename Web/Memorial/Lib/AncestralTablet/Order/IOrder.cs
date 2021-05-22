using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Memorial.Core.Dtos;

namespace Memorial.Lib.Ancestor
{
    public interface IOrder : ITransaction
    {
        void SetOrder(string AF);

        bool Create(AncestralTabletTransactionDto ancestralTabletTransactionDto);

        bool Update(AncestralTabletTransactionDto ancestralTabletTransactionDto);

        bool Delete();
    }
}