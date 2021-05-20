using Memorial.Core.Dtos;

namespace Memorial.Lib.Cemetery
{
    public interface IReciprocate : ITransaction
    {
        void SetReciprocate(string AF);

        bool Create(CemeteryTransactionDto cemeteryTransactionDto);

        bool Update(CemeteryTransactionDto cemeteryTransactionDto);

        bool Delete();
    }
}