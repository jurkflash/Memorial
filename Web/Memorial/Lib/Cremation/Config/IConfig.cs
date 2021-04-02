using Memorial.Core.Domain;
using Memorial.Core.Dtos;
using System.Collections.Generic;

namespace Memorial.Lib.Cremation
{
    public interface IConfig
    {
        bool CreateCremation(CremationDto cremationDto);
        bool CreateItem(CremationItemDto cremationItemDto);
        bool DeleteCremation(int id);
        bool DeleteItem(int id);
        CremationDto GetCremationDto(int id);
        IEnumerable<CremationDto> GetCremationDtos();
        CremationItemDto GetItemDto(int id);
        IEnumerable<CremationItemDto> GetItemDtos();
        IEnumerable<CremationNumber> GetNumbers();
        bool UpdateCremation(CremationDto cremationDto);
        bool UpdateItem(CremationItemDto cremationItemDto);
    }
}