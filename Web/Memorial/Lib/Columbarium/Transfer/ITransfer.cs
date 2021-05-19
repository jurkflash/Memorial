using Memorial.Core.Dtos;

namespace Memorial.Lib.Columbarium
{
    public interface ITransfer : ITransaction
    {
        bool AllowNicheDeceasePairing(int nicheId, int applicantId);
        bool Create(ColumbariumTransactionDto columbariumTransactionDto);
        bool Delete();
        float GetPrice(int itemId);
        void NewNumber(int itemId);
        void SetTransfer(string AF);
        bool Update(ColumbariumTransactionDto columbariumTransactionDto);
    }
}