using System;
using System.Collections.Generic;
using System.Web.Http;
using Memorial.Core.Dtos;
using Memorial.Lib.Columbarium;

namespace Memorial.Controllers.Api
{
    [RoutePrefix("api/columbariums")]
    public class ColumbariumsController : ApiController
    {
        private readonly INiche _niche;
        private readonly IArea _area;
        private readonly ICentre _centre;
        private readonly IManage _manage;

        public ColumbariumsController(INiche niche, ICentre centre, IArea area, IManage manage)
        {
            _niche = niche;
            _area = area;
            _centre = centre;
            _manage = manage;
        }

        [Route("~/api/sites/{siteId:int}/columbariums/centres")]
        public IEnumerable<ColumbariumCentreDto> GetCentres(int siteId)
        {
            return _centre.GetCentreDtosBySite(siteId);
        }

        [Route("centres/{centreId:int}/areas")]
        public IEnumerable<ColumbariumAreaDto> GetAreas(int centreId)
        {
            return _area.GetAreaDtosByCentre(centreId);
        }

        [Route("areas/{areaId:int}/availableNiches")]
        [Route("availableNiches/{areaId:int}")]
        public IEnumerable<NicheDto> GetAvailableNichesByAreaId(int areaId)
        {
            return _niche.GetAvailableNicheDtosByAreaId(areaId);
        }

        [Route("niche/{id:int}")]
        public IHttpActionResult GetNiche(int id)
        {
            return Ok(_niche.GetNicheDto(id));
        }

        [Route("{itemId:int}/amount")]
        public IHttpActionResult GetAmount(int itemId, DateTime from, DateTime to)
        {
            if (from > to)
                return BadRequest();

            var result = _manage.GetAmount(itemId, from, to);

            if (result == -1)
                return BadRequest();
            else
                return Ok(result);
        }

    }
}
