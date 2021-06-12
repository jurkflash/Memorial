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
using Memorial.Lib.Site;

namespace Memorial.Controllers.Api
{
    [RoutePrefix("api/sites")]
    public class SitesController : ApiController
    {
        private readonly ISite _site;

        public SitesController(ISite site)
        {
            _site = site;
        }

        [HttpGet]
        [Route("")]
        public IEnumerable<SiteDto> GetSites()
        {
            return _site.GetSiteDtos();
        }

        [HttpGet]
        [Route("{id:int}")]
        public IHttpActionResult GetSite(int id)
        {
            return Ok(_site.GetSiteDto(id));
        }

        [HttpPost]
        public IHttpActionResult CreateSite(SiteDto siteDto)
        {
            if (siteDto == null || !ModelState.IsValid)
                return BadRequest();

            var id = _site.CreateSite(siteDto);

            if (id == 0)
                return InternalServerError();

            siteDto.Id = id;

            return Created(new Uri(Request.RequestUri + "/" + id), siteDto);
        }

        [HttpPut]
        [Route("{id:int}")]
        public IHttpActionResult UpdateSite(int id, SiteDto siteDto)
        {
            if (_site.UpdateSite(siteDto))
                return Ok();
            else
                return InternalServerError();
        }

        [HttpDelete]
        [Route("{id:int}")]
        public IHttpActionResult DeleteSite(int id)
        {
            if (_site.DeleteSite(id))
                return Ok();
            else
                return InternalServerError();
        }

    }
}
