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
using Memorial.Lib.Quadrangle;

namespace Memorial.Controllers.Api
{
    public class QuadranglesController : ApiController
    {
        private readonly IQuadrangle _quadrangle;
        private readonly IArea _area;
        private readonly ICentre _centre;

        public QuadranglesController(IQuadrangle quadrangle, ICentre centre, IArea area)
        {
            _quadrangle = quadrangle;
            _area = area;
            _centre = centre;
        }

        public IHttpActionResult GetCentre(byte siteId)
        {
            return Ok(_centre.GetCentreDtosBySite(siteId));
        }

        public IHttpActionResult GetArea(int centreId)
        {
            return Ok(_area.GetAreaDtosByCentre(centreId));
        }

        public IHttpActionResult GetAvailableQuadrangleByAreaId(int areaId)
        {
            return Ok(_quadrangle.GetAvailableQuadrangleDtosByAreaId(areaId));
        }

        public IHttpActionResult GetQuadrangle(int id)
        {
            return Ok(_quadrangle.GetQuadrangleDto(id));
        }

    }
}
