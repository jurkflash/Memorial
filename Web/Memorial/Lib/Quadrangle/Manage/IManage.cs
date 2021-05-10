using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Memorial.Core.Dtos;

namespace Memorial.Lib.Quadrangle
{
    public interface IManage : ITransaction
    {
        void SetManage(string AF);

        float GetPrice(int itemId);

        bool Create(QuadrangleTransactionDto quadrangleTransactionDto);

        bool Update(QuadrangleTransactionDto quadrangleTransactionDto);

        bool Delete();

        bool ChangeQuadrangle(int oldQuadrangleId, int newQuadrangleId);

        float GetAmount(int itemId, DateTime from, DateTime to);
    }
}