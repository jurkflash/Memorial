using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Memorial.Core.Dtos;

namespace Memorial.Lib.Quadrangle
{
    public interface ITransfer : ITransaction
    {
        void SetTransfer(int id);

        float GetPrice();

        bool Create();
    }
}