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
using Memorial.Lib.Columbarium;

namespace Memorial.Controllers.Api
{
    public class ColumbariumsController : ApiController
    {
        private readonly INiche _niche;
        private readonly IArea _area;
        private readonly ICentre _centre;

        public ColumbariumsController(INiche niche, ICentre centre, IArea area)
        {
            _niche = niche;
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

        public IHttpActionResult GetAvailableNicheByAreaId(int areaId)
        {
            return Ok(_niche.GetAvailableNicheDtosByAreaId(areaId));
        }

        public IHttpActionResult GetNiche(int id)
        {
            return Ok(_niche.GetNicheDto(id));
        }

    }
}
