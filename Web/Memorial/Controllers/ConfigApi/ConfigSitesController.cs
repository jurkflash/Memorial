using System;
using System.Web.Http;
using Memorial.Core.Dtos;
using Memorial.Lib.Site;

namespace Memorial.Controllers.Api
{
    public class ConfigSitesController : ApiController
    {
        private readonly IConfig _config;

        public ConfigSitesController(IConfig config)
        {
            _config = config;
        }

        public IHttpActionResult GetSites()
        {
            return Ok(_config.GetSiteDtos());
        }

        public IHttpActionResult GetSite(byte id)
        {
            return Ok(_config.GetSiteDto(id));
        }

        [HttpPost]
        public IHttpActionResult CreateSite(SiteDto siteDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var id = _config.CreateSite(siteDto);

            if (id == 0)
                return InternalServerError();

            return Created(new Uri(Request.RequestUri + "/" + id), siteDto);
        }

        [HttpPut]
        public IHttpActionResult UpdateAncestralTablet(SiteDto siteDto)
        {
            if (_config.UpdateSite(siteDto))
                return Ok();
            else
                return InternalServerError();
        }

        [HttpDelete]
        public IHttpActionResult DeleteSite(byte id)
        {
            if (_config.DeleteSite(id))
                return Ok();
            else
                return InternalServerError();
        }
    }
}
