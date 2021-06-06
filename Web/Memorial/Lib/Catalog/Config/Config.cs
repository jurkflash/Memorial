using Memorial.Core;
using System.Collections.Generic;
using Memorial.Core.Dtos;

namespace Memorial.Lib.Catalog
{
    public class Config : IConfig
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICatalog _catalog;

        public Config(
            IUnitOfWork unitOfWork,
            ICatalog catalog
            )
        {
            _unitOfWork = unitOfWork;
            _catalog = catalog;
        }

        public CatalogDto GetCatalogDto(int id)
        {
            return _catalog.GetCatalogDto(id);
        }

        public IEnumerable<CatalogDto> GetCatalogDtos()
        {
            return _catalog.GetCatalogDtos();
        }

        public int CreateCatalog(CatalogDto catalogDto)
        {
            var catalog = _catalog.CreateCatalog(catalogDto);

            _unitOfWork.Complete();

            return catalog.Id;
        }

        public bool DeleteCatalog(int id)
        {
            if (_catalog.DeleteCatalog(id))
            {
                _unitOfWork.Complete();
                return true;
            }

            return false;
        }


    }
}