using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Memorial.Core.Dtos;

namespace Memorial.Lib.Cremation
{
    public interface IOrder : ITransaction
    {
        void SetOrder(string AF);

        bool Create(CremationTransactionDto cremationTransactionDto);

        bool Update(CremationTransactionDto cremationTransactionDto);

        bool Delete();
    }
}