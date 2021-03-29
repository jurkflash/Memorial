using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Memorial.Core.Dtos;

namespace Memorial.Lib.Space
{
    public interface IBooking : ITransaction
    {
        bool IsAvailable(int itemId, DateTime from, DateTime to);

        bool IsAvailable(string AF, DateTime from, DateTime to);

        void SetBooking(string AF);

        bool Create(SpaceTransactionDto spaceTransactionDto);

        bool Update(SpaceTransactionDto spaceTransactionDto);

        bool Delete();
    }
}