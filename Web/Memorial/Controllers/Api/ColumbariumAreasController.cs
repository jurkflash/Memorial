using System;
using System.Web.Http;
using System.Collections.Generic;
using Memorial.Core.Dtos;
using Memorial.Lib.Columbarium;

namespace Memorial.Controllers.Api
{
    [RoutePrefix("api/columbariums/centres/areas")]
    public class ColumbariumAreasController : ApiController
    {
        private readonly IArea _area;

        public ColumbariumAreasController(IArea area)
        {
            _area = area;
        }

        [Route("~/api/columbariums/centres/{centreId:int}/areas")]
        [HttpGet]
        public IEnumerable<ColumbariumAreaDto> GetAreas(int centreId)
        {
            return _area.GetAreaDtosByCentre(centreId);
        }

        [Route("{id:int}")]
        [HttpGet]
        public IHttpActionResult GetArea(int id)
        {
            return Ok(_area.GetAreaDto(id));
        }

        [Route("")]
        [HttpPost]
        public IHttpActionResult CreateArea(ColumbariumAreaDto areaDto)
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
        public IHttpActionResult UpdateArea(int id, ColumbariumAreaDto areaDto)
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
