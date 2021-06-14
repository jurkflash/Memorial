using System;
using System.Web.Http;
using System.Collections.Generic;
using Memorial.Core.Dtos;
using Memorial.Lib.Columbarium;

namespace Memorial.Controllers.Api
{
    [RoutePrefix("api/columbariums/centres")]
    public class ColumbariumCentresController : ApiController
    {
        private readonly ICentre _centre;

        public ColumbariumCentresController(ICentre centre)
        {
            _centre = centre;
        }

        [Route("~/api/sites/{siteId:int}/columbariums/centres")]
        [HttpGet]
        public IEnumerable<ColumbariumCentreDto> GetCentres(int siteId)
        {
            return _centre.GetCentreDtosBySite(siteId);
        }

        [Route("{id:int}")]
        [HttpGet]
        public IHttpActionResult GetCentre(int id)
        {
            return Ok(_centre.GetCentreDto(id));
        }

        [Route("")]
        [HttpPost]
        public IHttpActionResult CreateCentre(ColumbariumCentreDto centreDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var id = _centre.Create(centreDto);

            if (id == 0)
                return InternalServerError();

            centreDto.Id = id;

            return Created(new Uri(Request.RequestUri + "/" + id), centreDto);
        }

        [Route("{id:int}")]
        [HttpPut]
        public IHttpActionResult UpdateCentre(int id, ColumbariumCentreDto centreDto)
        {
            if (_centre.Update(centreDto))
                return Ok();
            else
                return InternalServerError();
        }

        [Route("{id:int}")]
        [HttpDelete]
        public IHttpActionResult DeleteCentre(int id)
        {
            if (_centre.Delete(id))
                return Ok();
            else
                return InternalServerError();
        }
    }
}
