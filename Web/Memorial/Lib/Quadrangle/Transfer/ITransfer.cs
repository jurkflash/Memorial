using Memorial.Core.Dtos;

namespace Memorial.Lib.Quadrangle
{
    public interface ITransfer : ITransaction
    {
        bool AllowQuadrangleDeceasePairing(int quadrangleId, int applicantId);
        bool Create(QuadrangleTransactionDto quadrangleTransactionDto);
        bool Delete();
        float GetPrice(int itemId);
        void NewNumber(int itemId);
        void SetTransfer(string AF);
        bool Update(QuadrangleTransactionDto quadrangleTransactionDto);
    }
}