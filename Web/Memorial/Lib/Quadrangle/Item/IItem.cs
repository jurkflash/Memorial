using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Memorial.Core.Dtos;

namespace Memorial.Lib.Quadrangle
{
    public interface IItem
    {
        void SetItem(int id);

        Core.Domain.QuadrangleItem GetItem();

        int GetId();

        string GetName();

        string GetDescription();

        float GetPrice();

        string GetSystemCode();

        bool IsOrder();

        IEnumerable<Core.Domain.QuadrangleItem> GetByCentre(int centreId);
    }
}