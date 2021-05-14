using Memorial.Core.Dtos;

namespace Memorial.Lib.Plot
{
    public interface IReciprocate : ITransaction
    {
        void SetReciprocate(string AF);

        bool Create(PlotTransactionDto plotTransactionDto);

        bool Update(PlotTransactionDto plotTransactionDto);

        bool Delete();
    }
}