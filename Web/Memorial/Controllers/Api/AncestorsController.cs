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
    public class AncestralTabletsController : ApiController
    {
        private readonly IAncestralTablet _ancestralTablet;
        private readonly IArea _area;

        public AncestralTabletsController(IAncestralTablet ancestralTablet, IArea area)
        {
            _ancestralTablet = ancestralTablet;
            _area = area;
        }

        public IHttpActionResult GetArea(byte siteId)
        {
            return Ok(_area.GetAreaDtosBySite(siteId));
        }

        public IHttpActionResult GetAvailableAncestralTabletByAreaId(int areaId)
        {
            return Ok(_ancestralTablet.GetAvailableAncestralTabletDtosByAreaId(areaId));
        }

        public IHttpActionResult GetAncestralTablet(int id)
        {
            return Ok(_ancestralTablet.GetAncestralTabletDto(id));
        }

    }
}
