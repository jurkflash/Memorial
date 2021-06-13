using System;
using System.Web.Http;
using System.Collections.Generic;
using Memorial.Core.Dtos;
using Memorial.Lib.Cemetery;

namespace Memorial.Controllers.Api
{
    [RoutePrefix("api/cemeteries/areas")]
    public class CemeteryAreasController : ApiController
    {
        private readonly IArea _area;

        public CemeteryAreasController(IArea area)
        {
            _area = area;
        }

        [Route("")]
        [HttpGet]
        public IHttpActionResult GetAreas()
        {
            return Ok(_area.GetAreaDtos());
        }

        [Route("{id:int}")]
        [HttpGet]
        public IHttpActionResult GetArea(int id)
        {
            return Ok(_area.GetAreaDto(id));
        }

        [Route("~/api/sites/{siteId:int}/cemeteries/areas")]
        [HttpGet]
        public IEnumerable<CemeteryAreaDto> GetAreaBySite(int siteId)
        {
            return _area.GetAreaDtosBySite(siteId);
        }

        [Route("")]
        [HttpPost]
        public IHttpActionResult CreateArea(CemeteryAreaDto areaDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var id = _area.Create(areaDto);

            if (id == 0)
                return InternalServerError();

            areaDto.Id = id;

            return Created(new Uri(Request.RequestUri + "/" + id), areaDto);
        }

        [Route("{id:int}")]
        [HttpPut]
        public IHttpActionResult UpdateArea(int id, CemeteryAreaDto areaDto)
        {
            if (_area.Update(areaDto))
                return Ok();
            else
                return InternalServerError();
        }

        [Route("{id:int}")]
        [HttpDelete]
        public IHttpActionResult DeleteArea(int id)
        {
            if (_area.Delete(id))
                return Ok();
            else
                return InternalServerError();
        }
    }
}
