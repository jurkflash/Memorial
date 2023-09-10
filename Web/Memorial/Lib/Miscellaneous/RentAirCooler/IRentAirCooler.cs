using Memorial.Core.Dtos;

namespace Memorial.Lib.Miscellaneous
{
    public interface IRentAirCooler : ITransaction
    {
        bool Add(Core.Domain.MiscellaneousTransaction miscellaneousTransaction);
        bool Change(string AF, Core.Domain.MiscellaneousTransaction miscellaneousTransaction);
        bool Remove(string AF);
    }
}