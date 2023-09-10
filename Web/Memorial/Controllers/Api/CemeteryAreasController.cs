using System;
using System.Web.Http;
using System.Collections.Generic;
using Memorial.Core.Dtos;
using Memorial.Lib.Cemetery;
using AutoMapper;

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
        public IHttpActionResult GetAll()
        {
            return Ok(Mapper.Map<IEnumerable<CemeteryAreaDto>>(_area.GetAll()));
        }

        [Route("{id:int}")]
        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            return Ok(Mapper.Map<CemeteryAreaDto>(_area.GetById(id)));
        }

        [Route("~/api/sites/{siteId:int}/cemeteries/areas")]
        [HttpGet]
        public IEnumerable<CemeteryAreaDto> GetBySite(int siteId)
        {
            return Mapper.Map<IEnumerable<CemeteryAreaDto>>(_area.GetBySite(siteId));
        }

        [Route("")]
        [HttpPost]
        public IHttpActionResult Add(CemeteryAreaDto areaDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var area = Mapper.Map<Core.Domain.CemeteryArea>(areaDto);
            var id = _area.Add(area);

            if (id == 0)
                return InternalServerError();

            areaDto.Id = id;

            return Created(new Uri(Request.RequestUri + "/" + id), areaDto);
        }

        [Route("{id:int}")]
        [HttpPut]
        public IHttpActionResult Change(int id, CemeteryAreaDto areaDto)
        {
            var area = Mapper.Map<Core.Domain.CemeteryArea>(areaDto);
            if (_area.Change(id, area))
                return Ok();
            else
                return InternalServerError();
        }

        [Route("{id:int}")]
        [HttpDelete]
        public IHttpActionResult DeleteArea(int id)
        {
            if (_area.Remove(id))
                return Ok();
            else
                return InternalServerError();
        }
    }
}
