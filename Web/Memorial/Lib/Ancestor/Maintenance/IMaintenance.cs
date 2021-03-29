using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Memorial.Core.Dtos;

namespace Memorial.Lib.Ancestor
{
    public interface IMaintenance : ITransaction
    {
        void SetMaintenance(string AF);

        float GetPrice(int itemId);

        bool Create(AncestorTransactionDto ancestorTransactionDto);

        bool Update(AncestorTransactionDto ancestorTransactionDto);

        bool Delete();

        float GetAmount(int itemId, DateTime from, DateTime to);
    }
}