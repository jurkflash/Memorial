using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Memorial.Core.Dtos;

namespace Memorial.Lib.Columbarium
{
    public interface IManage : ITransaction
    {
        void SetManage(string AF);

        float GetPrice(int itemId);

        bool Create(ColumbariumTransactionDto quadrangleTransactionDto);

        bool Update(ColumbariumTransactionDto quadrangleTransactionDto);

        bool Delete();

        bool ChangeQuadrangle(int oldQuadrangleId, int newQuadrangleId);

        float GetAmount(int itemId, DateTime from, DateTime to);
    }
}