﻿using System;
using System.Web.Http;
using System.Collections.Generic;
using Memorial.Core.Dtos;
using Memorial.Lib.AncestralTablet;

namespace Memorial.Controllers.Api
{
    [RoutePrefix("api/ancestraltablets/areas")]
    public class AncestralTabletAreasController : ApiController
    {
        private readonly IArea _area;

        public AncestralTabletAreasController(IArea area)
        {
            _area = area;
        }

        [Route("")]
        [HttpGet]
        public IHttpActionResult GetAreas()
        {
            return Ok(_area.GetAreaDtos());
        }

        [Route("{id:int}")]
        [HttpGet]
        public IHttpActionResult GetArea(int id)
        {
            return Ok(_area.GetAreaDto(id));
        }

        [Route("~/api/sites/{siteId:int}/ancestraltablets/areas")]
        [HttpGet]
        public IEnumerable<AncestralTabletAreaDto> GetAreaBySite(int siteId)
        {
            return _area.GetAreaDtosBySite(siteId);
        }

        [Route("")]
        [HttpPost]
        public IHttpActionResult CreateArea(AncestralTabletAreaDto areaDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var id = _area.Create(areaDto);

            if (id == 0)
                return InternalServerError();

            areaDto.Id = id;

            return Created(new Uri(Request.RequestUri + "/" + id), areaDto);
        }

        [Route("{id:int}")]
        [HttpPut]
        public IHttpActionResult UpdateArea(int id, AncestralTabletAreaDto areaDto)
        {
            if (_area.Update(areaDto))
                return Ok();
            else
                return InternalServerError();
        }

        [Route("{id:int}")]
        [HttpDelete]
        public IHttpActionResult DeleteArea(int id)
        {
            if (_area.Delete(id))
                return Ok();
            else
                return InternalServerError();
        }
    }
}