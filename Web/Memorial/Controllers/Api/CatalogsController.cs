using System;
using System.Web.Http;
using System.Collections.Generic;
using Memorial.Core.Dtos;
using Memorial.Lib.Catalog;

namespace Memorial.Controllers.Api
{
    [RoutePrefix("api/catalogs")]
    public class CatalogsController : ApiController
    {
        private readonly ICatalog _catalog;

        public CatalogsController(ICatalog catalog)
        {
            _catalog = catalog;
        }

        public IEnumerable<CatalogDto> GetCatalogs()
        {
            return _catalog.GetCatalogDtos();
        }

        public IHttpActionResult GetCatalog(int id)
        {
            return Ok(_catalog.GetCatalogDto(id));
        }

        [HttpPost]
        public IHttpActionResult CreateCatalog(CatalogDto catalogDto)
        {
            if (catalogDto == null || !ModelState.IsValid)
                return BadRequest();

            var id = _catalog.CreateCatalog(catalogDto);

            if (id == 0)
                return InternalServerError();

            return Created(new Uri(Request.RequestUri + "/" + id), catalogDto);
        }

        [HttpDelete]
        public IHttpActionResult DeleteCatalog(byte id)
        {
            if (_catalog.DeleteCatalog(id))
                return Ok();
            else
                return InternalServerError();
        }
    }
}
