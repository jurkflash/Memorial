﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Memorial.Core.Dtos;

namespace Memorial.Lib.Space
{
    public interface IItem
    {
        void SetItem(int id);

        Core.Domain.SpaceItem GetItem();

        SpaceItemDto GetItemDto();

        Core.Domain.SpaceItem GetItem(int id);

        SpaceItemDto GetItemDto(int id);

        int GetId();

        string GetName();

        string GetDescription();

        float GetPrice();

        string GetSystemCode();

        bool IsOrder();

        bool AllowDoubleBook();

        IEnumerable<Core.Domain.SpaceItem> GetItemBySpace(int spaceId);

        IEnumerable<SpaceItemDto> GetItemDtosBySpace(int spaceId);

    }
}