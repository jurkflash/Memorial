using System;
using Memorial.Core.Dtos;

namespace Memorial.Lib.Columbarium
{
    public interface IWithdraw : ITransaction
    {
        bool Create(ColumbariumTransactionDto columbariumTransactionDto);

        bool Update(ColumbariumTransactionDto columbariumTransactionDto);

        bool Delete();

        bool RemoveWithdrew(int nicheId);
    }
}