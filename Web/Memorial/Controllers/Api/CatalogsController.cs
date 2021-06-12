using System;
using System.Web.Http;
using System.Collections.Generic;
using Memorial.Core.Dtos;
using Memorial.Lib.Catalog;

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
        public IEnumerable<CatalogDto> GetCatalogs()
        {
            return _catalog.GetCatalogDtos();
        }

        [Route("~/api/sites/{siteId:int}/catalogs")]
        public IEnumerable<CatalogDto> GetCatalogsBySite(int siteId)
        {
            return _catalog.GetCatalogDtosBySite(siteId);
        }

        [Route("~/api/sites/{siteId:int}/availablecatalogs")]
        public IEnumerable<ProductDto> GetAvailableCatalogsBySite(int siteId)
        {
            return _catalog.GetAvailableCatalogDtosBySite(siteId);
        }

        [Route("{id:int}")]
        public IHttpActionResult GetCatalog(int id)
        {
            return Ok(_catalog.GetCatalogDto(id));
        }

        [Route("~/api/ancestraltablet/sites")]
        public IEnumerable<SiteDto> GetAncestralTabletSites()
        {
            return _catalog.GetSiteDtosAncestralTablet();
        }

        [Route("~/api/cemetery/sites")]
        public IEnumerable<SiteDto> GetCemeterySites()
        {
            return _catalog.GetSiteDtosCemetery();
        }

        [Route("~/api/cremation/sites")]
        public IEnumerable<SiteDto> GetCremationSites()
        {
            return _catalog.GetSiteDtosCremation();
        }

        [Route("~/api/urn/sites")]
        public IEnumerable<SiteDto> GetUrnSites()
        {
            return _catalog.GetSiteDtosUrn();
        }

        [Route("~/api/columbarium/sites")]
        public IEnumerable<SiteDto> GetColumbariumSites()
        {
            return _catalog.GetSiteDtosColumbarium();
        }

        [Route("~/api/space/sites")]
        public IEnumerable<SiteDto> GetSpaceSites()
        {
            return _catalog.GetSiteDtosSpace();
        }

        [Route("~/api/miscellaneous/sites")]
        public IEnumerable<SiteDto> GetMiscellaneousSites()
        {
            return _catalog.GetSiteDtosMiscellaneous();
        }

        [HttpPost]
        public IHttpActionResult CreateCatalog(CatalogDto catalogDto)
        {
            if (catalogDto == null || !ModelState.IsValid)
                return BadRequest();

            var id = _catalog.CreateCatalog(catalogDto);

            if (id == 0)
                return InternalServerError();

            return Created(new Uri(Request.RequestUri + "/" + id), catalogDto);
        }

        [HttpDelete]
        public IHttpActionResult DeleteCatalog(int id)
        {
            if (_catalog.DeleteCatalog(id))
                return Ok();
            else
                return InternalServerError();
        }
    }
}
