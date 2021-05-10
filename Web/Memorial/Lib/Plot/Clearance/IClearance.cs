using Memorial.Core.Dtos;

namespace Memorial.Lib.Plot
{
    public interface IClearance
    {
        bool Create(PlotTransactionDto plotTransactionDto);
        bool Delete();
        void NewNumber(int itemId);
        void SetClearance(string AF);
        bool Update(PlotTransactionDto plotTransactionDto);
    }
}