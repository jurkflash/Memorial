using Memorial.Core.Dtos;

namespace Memorial.Lib.Columbarium
{
    public interface IPhoto : ITransaction
    {
        void SetPhoto(string AF);

        bool Create(ColumbariumTransactionDto columbariumTransactionDto);

        bool Update(ColumbariumTransactionDto columbariumTransactionDto);

        bool Delete();

        bool ChangeNiche(int oldNicheId, int newNicheId);
    }
}