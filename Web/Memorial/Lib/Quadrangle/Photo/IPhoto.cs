using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Memorial.Core.Dtos;

namespace Memorial.Lib.Quadrangle
{
    public interface IPhoto : ITransaction
    {
        void SetPhoto(int id);

        float GetPrice();

        bool Create();
    }
}