using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Memorial.Core.Dtos;

namespace Memorial.Lib.Space
{
    public interface IHouse : ITransaction
    {
        void SetHouse(string AF);

        bool Create(SpaceTransactionDto spaceTransactionDto);

        bool Update(SpaceTransactionDto spaceTransactionDto);

        bool Delete();
    }
}