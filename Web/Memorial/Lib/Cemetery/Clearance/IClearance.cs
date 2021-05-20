using Memorial.Core.Dtos;

namespace Memorial.Lib.Cemetery
{
    public interface IClearance : ITransaction
    {
        bool Create(CemeteryTransactionDto cemeteryTransactionDto);
        bool Delete();
        void SetClearance(string AF);
        bool Update(CemeteryTransactionDto cemeteryTransactionDto);
    }
}