using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Memorial.Core.Dtos;

namespace Memorial.Lib.Urn
{
    public interface IPurchase : ITransaction
    {
        void SetOrder(string AF);

        bool Create(UrnTransactionDto urnTransactionDto);

        bool Update(UrnTransactionDto urnTransactionDto);

        bool Delete();
    }
}