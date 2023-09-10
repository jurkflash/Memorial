using System;
using System.Web.Http;
using System.Collections.Generic;
using Memorial.Core.Dtos;
using Memorial.Lib.Columbarium;
using AutoMapper;

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
        public IEnumerable<ColumbariumAreaDto> GetByCentre(int centreId)
        {
            return Mapper.Map<IEnumerable<ColumbariumAreaDto>>(_area.GetByCentre(centreId));
        }

        [Route("{id:int}")]
        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            return Ok(Mapper.Map<ColumbariumAreaDto>(_area.GetById(id)));
        }

        [Route("")]
        [HttpPost]
        public IHttpActionResult Add(ColumbariumAreaDto areaDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var columbariumArea = Mapper.Map<Core.Domain.ColumbariumArea>(areaDto);
            var id = _area.Add(columbariumArea);

            if (id == 0)
                return InternalServerError();

            areaDto.Id = id;

            return Created(new Uri(Request.RequestUri + "/" + id), areaDto);
        }

        [Route("{id:int}")]
        [HttpPut]
        public IHttpActionResult Change(int id, ColumbariumAreaDto areaDto)
        {
            var columbariumArea = Mapper.Map<Core.Domain.ColumbariumArea>(areaDto);
            if (_area.Change(id, columbariumArea))
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
