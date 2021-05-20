using Memorial.Core.Dtos;

namespace Memorial.Lib.Cemetery
{
    public interface IReciprocate : ITransaction
    {
        void SetReciprocate(string AF);

        bool Create(CemeteryTransactionDto plotTransactionDto);

        bool Update(CemeteryTransactionDto plotTransactionDto);

        bool Delete();
    }
}