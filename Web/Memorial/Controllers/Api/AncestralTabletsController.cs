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

        [Route("~/api/sites/{siteId:int}/ancestraltablets/areas")]
        public IEnumerable<AncestralTabletAreaDto> GetArea(int siteId)
        {
            return _area.GetAreaDtosBySite(siteId);
        }

        [Route("areas/{areaId:int}/availableAncestralTablets")]
        public IEnumerable<AncestralTabletDto> GetAvailableAncestralTabletByAreaId(int areaId)
        {
            return _ancestralTablet.GetAvailableAncestralTabletDtosByAreaId(areaId);
        }

        [Route("{id:int}")]
        public IHttpActionResult GetAncestralTablet(int id)
        {
            return Ok(_ancestralTablet.GetAncestralTabletDto(id));
        }

        [Route("{itemId:int}/amount")]
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

    }
}
