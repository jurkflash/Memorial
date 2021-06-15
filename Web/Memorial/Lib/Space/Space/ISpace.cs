using Memorial.Core.Dtos;
using System;
using System.Collections.Generic;

namespace Memorial.Lib.Space
{
    public interface ISpace
    {
        bool CheckAvailability(DateTime from, DateTime to, int spaceItemId);
        bool CheckAvailability(DateTime from, DateTime to, string AF);
        int Create(SpaceDto spaceDto);
        bool Delete(int id);
        IEnumerable<Core.Domain.Space> GetSpacesBySite(int siteId);
        IEnumerable<SpaceDto> GetSpaceDtosBySite(int siteId);
        double GetAmount(DateTime from, DateTime to, int spaceItemId);
        Core.Domain.Space GetSpace();
        Core.Domain.Space GetSpace(int id);
        SpaceDto GetSpaceDto(int id);
        SpaceDto GetSpaceDto();
        IEnumerable<SpaceDto> GetSpaceDtos();
        void SetSpace(int id);
        bool Update(SpaceDto spaceDto);
    }
}