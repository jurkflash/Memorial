using Memorial.Core.Dtos;
using System.Collections.Generic;

namespace Memorial.Lib.Catalog
{
    public interface IConfig
    {
        int CreateCatalog(CatalogDto catalogDto);
        bool DeleteCatalog(int id);
        CatalogDto GetCatalogDto(int id);
        IEnumerable<CatalogDto> GetCatalogDtos();
    }
}