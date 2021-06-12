using System;
using System.Web.Http;
using System.Collections.Generic;
using Memorial.Core.Dtos;
using Memorial.Lib.AncestralTablet;

namespace Memorial.Controllers.Api
{
    [RoutePrefix("api/ancestraltablets/areas")]
    public class AncestralTabletAreasController : ApiController
    {
        private readonly IArea _area;

        public AncestralTabletAreasController(IArea area)
        {
            _area = area;
        }

        [HttpGet]
        [Route("")]
        public IHttpActionResult GetAreas()
        {
            return Ok(_area.GetAreaDtos());
        }

        [HttpGet]
        [Route("{id:int}")]
        public IHttpActionResult GetArea(int id)
        {
            return Ok(_area.GetAreaDto(id));
        }

        [HttpGet]
        [Route("~/api/sites/{siteId:int}/ancestraltablets/areas")]
        public IEnumerable<AncestralTabletAreaDto> GetAreaBySite(int siteId)
        {
            return _area.GetAreaDtosBySite(siteId);
        }

        [HttpPost]
        public IHttpActionResult CreateArea(AncestralTabletAreaDto areaDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var id = _area.Create(areaDto);

            if (id == 0)
                return InternalServerError();

            areaDto.Id = id;

            return Created(new Uri(Request.RequestUri + "/" + id), areaDto);
        }

        [HttpPut]
        [Route("{id:int}")]
        public IHttpActionResult UpdateArea(int id, AncestralTabletAreaDto areaDto)
        {
            if (_area.Update(areaDto))
                return Ok();
            else
                return InternalServerError();
        }

        [HttpDelete]
        [Route("{id:int}")]
        public IHttpActionResult DeleteArea(int id)
        {
            if (_area.Delete(id))
                return Ok();
            else
                return InternalServerError();
        }
    }
}
