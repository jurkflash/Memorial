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
using Memorial.Lib.Ancestor;

namespace Memorial.Controllers.Api
{
    public class AncestorsController : ApiController
    {
        private readonly IAncestor _ancestor;
        private readonly IArea _area;

        public AncestorsController(IAncestor ancestor, IArea area)
        {
            _ancestor = ancestor;
            _area = area;
        }

        public IHttpActionResult GetArea(byte siteId)
        {
            return Ok(_area.GetAreaDtosBySite(siteId));
        }

        public IHttpActionResult GetAvailableAncestorByAreaId(int areaId)
        {
            return Ok(_ancestor.GetAvailableAncestorDtosByAreaId(areaId));
        }

        public IHttpActionResult GetAncestor(int id)
        {
            return Ok(_ancestor.GetAncestorDto(id));
        }

    }
}
