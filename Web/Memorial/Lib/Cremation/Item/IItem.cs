using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Memorial.Core.Dtos;

namespace Memorial.Lib.Cremation
{
    public interface IItem
    {
        void SetItem(int id);

        Core.Domain.CremationItem GetItem();

        CremationItemDto GetItemDto();

        Core.Domain.CremationItem GetItem(int id);

        CremationItemDto GetItemDto(int id);

        IEnumerable<CremationItemDto> GetItemDtos();

        int GetId();

        int GetCremationId();

        string GetName();

        string GetDescription();

        float GetPrice();

        string GetSystemCode();

        bool IsOrder();

        IEnumerable<Core.Domain.CremationItem> GetItemByCremation(int cremationId);

        IEnumerable<CremationItemDto> GetItemDtosByCremation(int cremationId);

        bool Create(CremationItemDto cremationItemDto);

        bool Update(Core.Domain.CremationItem cremationItem);

        bool Delete(int id);
    }
}