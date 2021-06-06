using System;
using System.Web.Http;
using Memorial.Core.Dtos;
using Memorial.Lib.AncestralTablet;

namespace Memorial.Controllers.Api
{
    public class ConfigAncestralTabletAreasController : ApiController
    {
        private readonly IConfig _config;

        public ConfigAncestralTabletAreasController(IConfig config)
        {
            _config = config;
        }

        public IHttpActionResult GetAreas(byte siteId)
        {
            return Ok(_config.GetAreaDtos());
        }

        public IHttpActionResult GetArea(int id)
        {
            return Ok(_config.GetAreaDto(id));
        }

        [HttpPost]
        public IHttpActionResult CreateArea(AncestralTabletAreaDto areaDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var id = _config.CreateArea(areaDto);

            if (id == 0)
                return InternalServerError();

            return Created(new Uri(Request.RequestUri + "/" + id), areaDto);
        }

        [HttpPut]
        public IHttpActionResult UpdateArea(int id, AncestralTabletAreaDto areaDto)
        {
            if (_config.UpdateArea(areaDto))
                return Ok();
            else
                return InternalServerError();
        }

        [HttpDelete]
        public IHttpActionResult DeleteArea(int id)
        {
            if (_config.DeleteArea(id))
                return Ok();
            else
                return InternalServerError();
        }
    }
}
