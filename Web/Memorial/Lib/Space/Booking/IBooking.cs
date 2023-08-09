using System;
using Memorial.Core.Domain;

namespace Memorial.Lib.Space
{
    public interface IBooking : ITransaction
    {
        bool IsAvailable(int itemId, DateTime from, DateTime to);

        bool IsAvailable(string AF, DateTime from, DateTime to);

        bool Add(SpaceTransaction spaceTransaction);

        bool Change(string AF, SpaceTransaction spaceTransaction);

        bool Remove(string AF);
    }
}