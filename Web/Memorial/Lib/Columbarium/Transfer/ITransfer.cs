using Memorial.Core.Dtos;

namespace Memorial.Lib.Columbarium
{
    public interface ITransfer : ITransaction
    {
        bool AllowQuadrangleDeceasePairing(int quadrangleId, int applicantId);
        bool Create(ColumbariumTransactionDto quadrangleTransactionDto);
        bool Delete();
        float GetPrice(int itemId);
        void NewNumber(int itemId);
        void SetTransfer(string AF);
        bool Update(ColumbariumTransactionDto quadrangleTransactionDto);
    }
}