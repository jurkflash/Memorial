using Memorial.Core.Domain;
using Memorial.Core.Dtos;
using System.Collections.Generic;

namespace Memorial.Lib.Miscellaneous
{
    public interface IConfig
    {
        bool CreateItem(MiscellaneousItemDto miscellaneousItemDto);
        bool CreateMiscellaneous(MiscellaneousDto miscellaneousDto);
        bool DeleteItem(int id);
        bool DeleteMiscellaneous(int id);
        MiscellaneousItemDto GetItemDto(int id);
        IEnumerable<MiscellaneousItemDto> GetItemDtos();
        MiscellaneousDto GetMiscellaneousDto(int id);
        IEnumerable<MiscellaneousDto> GetMiscellaneousDtos();
        IEnumerable<MiscellaneousNumber> GetNumbers();
        bool UpdateItem(MiscellaneousItemDto miscellaneousItemDto);
        bool UpdateMiscellaneous(MiscellaneousDto miscellaneousDto);
    }
}