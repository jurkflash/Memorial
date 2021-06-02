using System;
using System.Web.Http;
using Memorial.Lib.AncestralTablet;

namespace Memorial.Controllers.Api
{
    public class AncestralTabletMaintenanceController : ApiController
    {
        private readonly IMaintenance _maintenance;

        public AncestralTabletMaintenanceController(IMaintenance maintenance)
        {
            _maintenance = maintenance;
        }

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
