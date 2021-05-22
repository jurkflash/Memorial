﻿using System.Collections.Generic;
using Memorial.Core.Dtos;

namespace Memorial.Lib.Ancestor
{
    public interface IArea
    {
        void SetArea(int id);

        int GetId();

        string GetName();

        string GetDescription();

        byte GetSiteId();

        Core.Domain.AncestralTabletArea GetArea();

        AncestralTabletAreaDto GetAreaDto();

        Core.Domain.AncestralTabletArea GetArea(int areaId);

        AncestralTabletAreaDto GetAreaDto(int areaId);

        IEnumerable<Core.Domain.AncestralTabletArea> GetAreas();

        IEnumerable<AncestralTabletAreaDto> GetAreaDtos();

        IEnumerable<Core.Domain.AncestralTabletArea> GetAreaBySite(byte siteId);

        IEnumerable<AncestralTabletAreaDto> GetAreaDtosBySite(byte siteId);

        bool Create(AncestralTabletAreaDto ancestralTabletAreaDto);

        bool Update(Core.Domain.AncestralTabletArea ancestralTabletArea);

        bool Delete(int id);

    }
}