using System;
using Memorial.Core.Dtos;

namespace Memorial.Lib.AncestralTablet
{
    public interface IWithdraw : ITransaction
    {
        bool Create(AncestralTabletTransactionDto ancestralTabletTransactionDto);

        bool Update(AncestralTabletTransactionDto ancestralTabletTransactionDto);

        bool Delete();

        bool RemoveWithdrew(int ancestralTabletId);
    }
}