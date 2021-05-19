using System;
using Memorial.Core.Dtos;

namespace Memorial.Lib.Columbarium
{
    public interface IManage : ITransaction
    {
        void SetManage(string AF);

        float GetPrice(int itemId);

        bool Create(ColumbariumTransactionDto columbariumTransactionDto);

        bool Update(ColumbariumTransactionDto columbariumTransactionDto);

        bool Delete();

        bool ChangeNiche(int oldNicheId, int newNicheId);

        float GetAmount(int itemId, DateTime from, DateTime to);
    }
}