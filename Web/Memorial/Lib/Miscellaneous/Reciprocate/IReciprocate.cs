using Memorial.Core.Dtos;

namespace Memorial.Lib.Miscellaneous
{
    public interface IReciprocate : ITransaction
    {
        void SetReciprocate(string AF);

        bool Create(MiscellaneousTransactionDto miscellaneousTransactionDto);

        bool Update(MiscellaneousTransactionDto miscellaneousTransactionDto);

        bool Delete();
    }
}