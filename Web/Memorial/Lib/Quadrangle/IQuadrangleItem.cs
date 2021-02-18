using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Memorial.Core.Dtos;

namespace Memorial.Lib
{
    public interface IQuadrangleItem
    {
        void SetById(int id);

        string GetSystemCode();

        IEnumerable<Core.Domain.QuadrangleItem> GetByCentre(int centreId);

        IEnumerable<QuadrangleItemDto> DtosGetByCentre(int centreId);

        bool GetOrderFlag();

        float GetPrice();

        float GetAmount(DateTime from, DateTime to, int itemId);

    }
}