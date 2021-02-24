using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Memorial.Core.Dtos;

namespace Memorial.Lib.Quadrangle
{
    public interface IManage : ITransaction
    {
        void SetManage(int id);

        float GetPrice();

        bool Create();

        float GetAmount(DateTime from, DateTime to);
    }
}