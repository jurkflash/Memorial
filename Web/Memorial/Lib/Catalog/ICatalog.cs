﻿using Memorial.Core.Dtos;
using System.Collections.Generic;

namespace Memorial.Lib.Catalog
{
    public interface ICatalog
    {
        int CreateCatalog(CatalogDto catalogDto);
        bool DeleteCatalog(int id);
        Core.Domain.Catalog GetCatalog();
        Core.Domain.Catalog GetCatalog(int id);
        CatalogDto GetCatalogDto();
        CatalogDto GetCatalogDto(int id);
        IEnumerable<CatalogDto> GetCatalogDtos();
        IEnumerable<CatalogDto> GetCatalogDtosBySite(int id);
        IEnumerable<Core.Domain.Catalog> GetCatalogs();
        IEnumerable<Core.Domain.Catalog> GetCatalogsBySite(int id);
        void SetCatalog(int id);
    }
}