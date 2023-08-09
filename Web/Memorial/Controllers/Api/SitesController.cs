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
using System.Security.Policy;

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

        [Route("")]
        [HttpGet]
        public IEnumerable<SiteDto> GetAll()
        {
            return Mapper.Map<IEnumerable<SiteDto>>(_site.GetAll());
        }

        [Route("{id:int}")]
        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            return Ok(Mapper.Map<SiteDto>(_site.Get(id)));
        }

        [Route("")]
        [HttpPost]
        public IHttpActionResult Add(SiteDto siteDto)
        {
            if (siteDto == null || !ModelState.IsValid)
                return BadRequest();

            var site = Mapper.Map<Core.Domain.Site>(siteDto);
            var id = _site.Add(site);

            if (id == 0)
                return InternalServerError();

            siteDto.Id = id;

            return Created(new Uri(Request.RequestUri + "/" + id), siteDto);
        }

        [Route("{id:int}")]
        [HttpPut]
        public IHttpActionResult Change(int id, SiteDto siteDto)
        {
            var site = Mapper.Map<Core.Domain.Site>(siteDto);
            if (_site.Change(id, site))
                return Ok();
            else
                return InternalServerError();
        }

        [Route("{id:int}")]
        [HttpDelete]
        public IHttpActionResult Remove(int id)
        {
            if (_site.Remove(id))
                return Ok();
            else
                return InternalServerError();
        }

    }
}
