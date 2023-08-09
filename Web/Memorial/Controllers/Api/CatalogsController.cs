using System;
using System.Web.Http;
using System.Collections.Generic;
using Memorial.Core.Dtos;
using Memorial.Lib.Catalog;
using AutoMapper;

namespace Memorial.Controllers.Api
{
    [RoutePrefix("api/catalogs")]
    public class CatalogsController : ApiController
    {
        private readonly ICatalog _catalog;

        public CatalogsController(ICatalog catalog)
        {
            _catalog = catalog;
        }

        [Route("")]
        [HttpGet]
        public IEnumerable<CatalogDto> GetAll()
        {
            return Mapper.Map<IEnumerable<CatalogDto>>(_catalog.GetAll());
        }

        [Route("~/api/sites/{siteId:int}/catalogs")]
        [HttpGet]
        public IEnumerable<CatalogDto> GetBySite(int siteId)
        {
            return Mapper.Map<IEnumerable<CatalogDto>>(_catalog.GetBySite(siteId));
        }

        [Route("~/api/sites/{siteId:int}/availablecatalogs")]
        [HttpGet]
        public IEnumerable<ProductDto> GetAvailableBySite(int siteId)
        {
            return Mapper.Map<IEnumerable<ProductDto>>(_catalog.GetAvailableBySite(siteId));
        }

        [Route("{id:int}")]
        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            return Ok(Mapper.Map<CatalogDto>(_catalog.Get(id)));
        }

        [Route("~/api/ancestraltablets/sites")]
        [HttpGet]
        public IEnumerable<SiteDto> GetAncestralTabletSites()
        {
            return Mapper.Map<IEnumerable<SiteDto>>(_catalog.GetSitesAncestralTablet());
        }

        [Route("~/api/cemeteries/sites")]
        [HttpGet]
        public IEnumerable<SiteDto> GetCemeterySites()
        {
            return Mapper.Map<IEnumerable<SiteDto>>(_catalog.GetSitesCemetery());
        }

        [Route("~/api/cremations/sites")]
        [HttpGet]
        public IEnumerable<SiteDto> GetCremationSites()
        {
            return Mapper.Map<IEnumerable<SiteDto>>(_catalog.GetSitesCremation());
        }

        [Route("~/api/urns/sites")]
        [HttpGet]
        public IEnumerable<SiteDto> GetUrnSites()
        {
            return Mapper.Map<IEnumerable<SiteDto>>(_catalog.GetSitesUrn());
        }

        [Route("~/api/columbariums/sites")]
        [HttpGet]
        public IEnumerable<SiteDto> GetColumbariumSites()
        {
            return Mapper.Map<IEnumerable<SiteDto>>(_catalog.GetSitesColumbarium());
        }

        [Route("~/api/spaces/sites")]
        [HttpGet]
        public IEnumerable<SiteDto> GetSpaceSites()
        {
            return Mapper.Map<IEnumerable<SiteDto>>(_catalog.GetSitesSpace());
        }

        [Route("~/api/miscellaneous/sites")]
        [HttpGet]
        public IEnumerable<SiteDto> GetMiscellaneousSites()
        {
            return Mapper.Map<IEnumerable<SiteDto>>(_catalog.GetSitesMiscellaneous());
        }

        [Route("")]
        [HttpPost]
        public IHttpActionResult Add(CatalogDto catalogDto)
        {
            if (catalogDto == null || !ModelState.IsValid)
                return BadRequest();

            var catalog = Mapper.Map<Core.Domain.Catalog>(catalogDto);
            var id = _catalog.Add(catalog);

            if (id == 0)
                return InternalServerError();

            return Created(new Uri(Request.RequestUri + "/" + id), catalogDto);
        }

        [Route("{id:int}")]
        [HttpDelete]
        public IHttpActionResult Remove(int id)
        {
            if (_catalog.Remove(id))
                return Ok();
            else
                return InternalServerError();
        }
    }
}
