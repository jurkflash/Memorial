using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Memorial.Core.Dtos;

namespace Memorial.Lib.AncestralTablet
{
    public interface IMaintenance : ITransaction
    {
        void SetMaintenance(string AF);

        float GetPrice(int itemId);

        bool Create(AncestralTabletTransactionDto ancestralTabletTransactionDto);

        bool Update(AncestralTabletTransactionDto ancestralTabletTransactionDto);

        bool Delete();

        bool ChangeAncestralTablet(int oldAncestralTabletId, int newAncestralTabletId);

        float GetAmount(int itemId, DateTime from, DateTime to);
    }
}