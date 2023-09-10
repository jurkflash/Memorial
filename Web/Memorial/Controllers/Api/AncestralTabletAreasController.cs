using System;
using System.Web.Http;
using System.Collections.Generic;
using Memorial.Core.Dtos;
using Memorial.Lib.AncestralTablet;
using AutoMapper;
using Memorial.Core.Domain;

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

        [Route("")]
        [HttpGet]
        public IHttpActionResult GetAll()
        {
            return Ok(Mapper.Map<IEnumerable<AncestralTabletAreaDto>>(_area.GetAll()));
        }

        [Route("{id:int}")]
        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            return Ok(Mapper.Map<AncestralTabletAreaDto>(_area.GetById(id)));
        }

        [Route("~/api/sites/{siteId:int}/ancestraltablets/areas")]
        [HttpGet]
        public IEnumerable<AncestralTabletAreaDto> GetBySite(int siteId)
        {
            return Mapper.Map<IEnumerable<AncestralTabletAreaDto>>(_area.GetBySite(siteId));
        }

        [Route("")]
        [HttpPost]
        public IHttpActionResult Add(AncestralTabletAreaDto areaDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var area = Mapper.Map<AncestralTabletArea>(areaDto);
            var id = _area.Add(area);

            if (id == 0)
                return InternalServerError();

            areaDto.Id = id;

            return Created(new Uri(Request.RequestUri + "/" + id), areaDto);
        }

        [Route("{id:int}")]
        [HttpPut]
        public IHttpActionResult Change(int id, AncestralTabletAreaDto areaDto)
        {
            var area = Mapper.Map<AncestralTabletArea>(areaDto);
            if (_area.Change(id, area))
                return Ok();
            else
                return InternalServerError();
        }

        [Route("{id:int}")]
        [HttpDelete]
        public IHttpActionResult Remove(int id)
        {
            if (_area.Remove(id))
                return Ok();
            else
                return InternalServerError();
        }
    }
}
