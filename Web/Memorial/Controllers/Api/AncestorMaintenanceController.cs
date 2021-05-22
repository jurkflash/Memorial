using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Memorial.Core;
using Memorial.Core.Domain;
using Memorial.Core.Dtos;
using AutoMapper;
using Memorial.Lib;
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
