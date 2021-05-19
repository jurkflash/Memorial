using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Memorial.Core.Dtos;

namespace Memorial.Lib.Columbarium
{
    public interface IOrder : ITransaction
    {
        void SetOrder(string AF);

        bool Create(ColumbariumTransactionDto columbariumTransactionDto);

        bool Update(ColumbariumTransactionDto columbariumTransactionDto);

        bool Delete();
    }
}