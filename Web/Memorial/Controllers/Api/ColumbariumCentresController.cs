using System;
using System.Web.Http;
using System.Collections.Generic;
using Memorial.Core.Dtos;
using Memorial.Lib.Columbarium;
using AutoMapper;

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
        public IEnumerable<ColumbariumCentreDto> GetBySite(int siteId)
        {
            return Mapper.Map<IEnumerable<ColumbariumCentreDto>>(_centre.GetBySite(siteId));
        }

        [Route("{id:int}")]
        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            return Ok(Mapper.Map<ColumbariumCentreDto>(_centre.GetById(id)));
        }

        [Route("")]
        [HttpPost]
        public IHttpActionResult CreateCentre(ColumbariumCentreDto centreDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var centre = Mapper.Map<Core.Domain.ColumbariumCentre>(centreDto);
            var id = _centre.Add(centre);

            if (id == 0)
                return InternalServerError();

            centreDto.Id = id;

            return Created(new Uri(Request.RequestUri + "/" + id), centreDto);
        }

        [Route("{id:int}")]
        [HttpPut]
        public IHttpActionResult Change(int id, ColumbariumCentreDto centreDto)
        {
            var centre = Mapper.Map<Core.Domain.ColumbariumCentre>(centreDto);
            if (_centre.Change(id, centre))
                return Ok();
            else
                return InternalServerError();
        }

        [Route("{id:int}")]
        [HttpDelete]
        public IHttpActionResult Remove(int id)
        {
            if (_centre.Remove(id))
                return Ok();
            else
                return InternalServerError();
        }
    }
}
