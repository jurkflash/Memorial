using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Memorial.Core.Dtos;

namespace Memorial.Lib.Cemetery
{
    public interface ISecondBurial : ITransaction
    {
        void SetSecondBurial(string AF);

        bool Create(CemeteryTransactionDto cemeteryTransactionDto);

        bool Update(CemeteryTransactionDto cemeteryTransactionDto);

        bool Delete();
    }
}