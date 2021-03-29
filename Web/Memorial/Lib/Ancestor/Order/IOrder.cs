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

        bool Create(AncestorTransactionDto ancestorTransactionDto);

        bool Update(AncestorTransactionDto ancestorTransactionDto);

        bool Delete();
    }
}