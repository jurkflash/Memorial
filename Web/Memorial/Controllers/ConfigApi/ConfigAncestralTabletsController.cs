using System;
using System.Web.Http;
using Memorial.Core.Dtos;
using Memorial.Lib.AncestralTablet;

namespace Memorial.Controllers.Api
{
    public class ConfigAncestralTabletsController : ApiController
    {
        private readonly IConfig _config;

        public ConfigAncestralTabletsController(IConfig config)
        {
            _config = config;
        }

        public IHttpActionResult GetAncestralTablets(int areaId)
        {
            return Ok(_config.GetAncestralTabletDtosByAreaId(areaId));
        }

        public IHttpActionResult GetAncestralTablet(int id)
        {
            return Ok(_config.GetAncestralTabletDto(id));
        }

        [HttpPost]
        public IHttpActionResult CreateAncestralTablet(AncestralTabletDto ancestralTabletDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var id = _config.CreateAncestralTablet(ancestralTabletDto);

            if (id == 0)
                return InternalServerError();

            return Created(new Uri(Request.RequestUri + "/" + id), ancestralTabletDto);
        }

        [HttpPut]
        public IHttpActionResult UpdateAncestralTablet(AncestralTabletDto ancestralTabletDto)
        {
            if (_config.UpdateAncestralTablet(ancestralTabletDto))
                return Ok();
            else
                return InternalServerError();
        }

        [HttpDelete]
        public IHttpActionResult DeleteAncestralTablet(int id)
        {
            if (_config.DeleteAncestralTablet(id))
                return Ok();
            else
                return InternalServerError();
        }
    }
}
