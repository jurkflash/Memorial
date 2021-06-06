using System;
using System.Web.Http;
using Memorial.Core.Dtos;
using Memorial.Lib.Catalog;

namespace Memorial.Controllers.Api
{
    public class ConfigCatalogsController : ApiController
    {
        private readonly IConfig _config;

        public ConfigCatalogsController(IConfig config)
        {
            _config = config;
        }

        public IHttpActionResult GetCatalogs()
        {
            return Ok(_config.GetCatalogDtos());
        }

        public IHttpActionResult GetCatalog(byte id)
        {
            return Ok(_config.GetCatalogDto(id));
        }

        [HttpPost]
        public IHttpActionResult CreateCatalog(CatalogDto catalogDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var id = _config.CreateCatalog(catalogDto);

            if (id == 0)
                return InternalServerError();

            return Created(new Uri(Request.RequestUri + "/" + id), catalogDto);
        }

        [HttpDelete]
        public IHttpActionResult DeleteCatalog(byte id)
        {
            if (_config.DeleteCatalog(id))
                return Ok();
            else
                return InternalServerError();
        }
    }
}
