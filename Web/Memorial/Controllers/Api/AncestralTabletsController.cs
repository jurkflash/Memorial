using System;
using System.Web.Http;
using Memorial.Core.Dtos;
using System.Collections.Generic;
using Memorial.Lib.AncestralTablet;
using AutoMapper;

namespace Memorial.Controllers.Api
{
    [RoutePrefix("api/ancestraltablets")]
    public class AncestralTabletsController : ApiController
    {
        private readonly IAncestralTablet _ancestralTablet;
        private readonly IMaintenance _maintenance;

        public AncestralTabletsController(IAncestralTablet ancestralTablet, IMaintenance maintenance)
        {
            _ancestralTablet = ancestralTablet;
            _maintenance = maintenance;
        }

        [Route("~/api/ancestraltablets/areas/{areaId:int}/availableAncestralTablets")]
        [HttpGet]
        public IEnumerable<AncestralTabletDto> GetAvailableAncestralTabletByAreaId(int areaId)
        {
            return Mapper.Map<IEnumerable<AncestralTabletDto>>(_ancestralTablet.GetAvailableAncestralTabletsByAreaId(areaId));
        }

        [Route("~/api/ancestraltablets/areas/{areaId:int}/ancestralTablets")]
        [HttpGet]
        public IEnumerable<AncestralTabletDto> GetByAreaId(int areaId)
        {
            return Mapper.Map<IEnumerable<AncestralTabletDto>>(_ancestralTablet.GetByAreaId(areaId));
        }

        [Route("~/api/ancestraltablets/areas/{areaId:int}/ancestralTablets")]
        [HttpGet]
        public IHttpActionResult GetByAreaIdAndPositions(int areaId, int positionX, int positionY)
        {
            return Ok(Mapper.Map<AncestralTabletDto>(_ancestralTablet.GetByAreaIdAndPostions(areaId, positionX, positionY)));
        }

        [Route("{id:int}")]
        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            return Ok(Mapper.Map<AncestralTabletDto>(_ancestralTablet.GetById(id)));
        }

        [Route("~/api/ancestraltablets/items/{itemId:int}/amount")]
        [HttpGet]
        public IHttpActionResult GetAmount(int itemId, DateTime from, DateTime to)
        {
            if (from > to)
                return BadRequest();

            var result = _maintenance.GetAmount(itemId, from, to);

            if (result == -1)
                return BadRequest();
            else
                return Ok(result);
        }

        [Route("")]
        [HttpPost]
        public IHttpActionResult Add(AncestralTabletDto ancestralTabletDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var ancestralTablet = Mapper.Map<Core.Domain.AncestralTablet>(ancestralTabletDto);
            var id = _ancestralTablet.Add(ancestralTablet);

            if (id == 0)
                return InternalServerError();

            ancestralTabletDto.Id = id;

            return Created(new Uri(Request.RequestUri + "/" + id), ancestralTabletDto);
        }

        [Route("{id:int}")]
        [HttpPut]
        public IHttpActionResult Change(int id, AncestralTabletDto ancestralTabletDto)
        {
            var ancestralTablet = Mapper.Map<Core.Domain.AncestralTablet>(ancestralTabletDto);
            if (_ancestralTablet.Change(id, ancestralTablet))
                return Ok();
            else
                return InternalServerError();
        }

        [Route("{id:int}")]
        [HttpDelete]
        public IHttpActionResult Remove(int id)
        {
            if (_ancestralTablet.Remove(id))
                return Ok();
            else
                return InternalServerError();
        }

    }
}
