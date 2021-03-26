using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Memorial.Core.Dtos;

namespace Memorial.Lib.Miscellaneous
{
    public interface IDonation : ITransaction
    {
        void SetOrder(string AF);

        bool Create(MiscellaneousTransactionDto miscellaneousTransactionDto);

        bool Update(MiscellaneousTransactionDto miscellaneousTransactionDto);

        bool Delete();
    }
}