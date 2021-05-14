using Memorial.Core.Dtos;

namespace Memorial.Lib.Plot
{
    public interface IClearance : ITransaction
    {
        bool Create(PlotTransactionDto plotTransactionDto);
        bool Delete();
        void SetClearance(string AF);
        bool Update(PlotTransactionDto plotTransactionDto);
    }
}