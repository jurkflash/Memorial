using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Memorial.Core.Dtos;

namespace Memorial.Lib.AncestralTablet
{
    public interface IShift : ITransaction
    {
        void SetShift(string AF);

        bool Create(AncestralTabletTransactionDto ancestralTabletTransactionDto);

        bool Update(AncestralTabletTransactionDto ancestralTabletTransactionDto);

        bool Delete();
    }
}