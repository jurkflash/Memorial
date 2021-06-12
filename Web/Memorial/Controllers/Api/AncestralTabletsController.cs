using System;
using System.Web.Http;
using Memorial.Core.Dtos;
using System.Collections.Generic;
using Memorial.Lib.AncestralTablet;

namespace Memorial.Controllers.Api
{
    [RoutePrefix("api/ancestraltablets")]
    public class AncestralTabletsController : ApiController
    {
        private readonly IAncestralTablet _ancestralTablet;
        private readonly IArea _area;
        private readonly IMaintenance _maintenance;

        public AncestralTabletsController(IAncestralTablet ancestralTablet, IArea area, IMaintenance maintenance)
        {
            _ancestralTablet = ancestralTablet;
            _area = area;
            _maintenance = maintenance;
        }

        [Route("~/api/ancestraltablets/areas/{areaId:int}/availableAncestralTablets")]
        [HttpGet]
        public IEnumerable<AncestralTabletDto> GetAvailableAncestralTabletByAreaId(int areaId)
        {
            return _ancestralTablet.GetAvailableAncestralTabletDtosByAreaId(areaId);
        }

        [Route("~/api/ancestraltablets/areas/{areaId:int}/ancestralTablets")]
        [HttpGet]
        public IEnumerable<AncestralTabletDto> GetAncestralTabletByAreaId(int areaId)
        {
            return _ancestralTablet.GetAncestralTabletDtosByAreaId(areaId);
        }

        [Route("~/api/ancestraltablets/areas/{areaId:int}/ancestralTablets")]
        [HttpGet]
        public IHttpActionResult GetAncestralTabletByAreaIdAndPositions(int areaId, int positionX, int positionY)
        {
            return Ok(_ancestralTablet.GetAncestralTabletDtoByAreaIdAndPostions(areaId, positionX, positionY));
        }

        [Route("{id:int}")]
        [HttpGet]
        public IHttpActionResult GetAncestralTablet(int id)
        {
            return Ok(_ancestralTablet.GetAncestralTabletDto(id));
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

        [HttpPost]
        public IHttpActionResult CreateAncestralTablet(AncestralTabletDto ancestralTabletDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var id = _ancestralTablet.Create(ancestralTabletDto);

            if (id == 0)
                return InternalServerError();

            ancestralTabletDto.Id = id;

            return Created(new Uri(Request.RequestUri + "/" + id), ancestralTabletDto);
        }

        [Route("{id:int}")]
        [HttpPut]
        public IHttpActionResult UpdateAncestralTablet(int id, AncestralTabletDto ancestralTabletDto)
        {
            if (_ancestralTablet.Update(ancestralTabletDto))
                return Ok();
            else
                return InternalServerError();
        }

        [Route("{id:int}")]
        [HttpDelete]
        public IHttpActionResult DeleteAncestralTablet(int id)
        {
            if (_ancestralTablet.Delete(id))
                return Ok();
            else
                return InternalServerError();
        }

    }
}
