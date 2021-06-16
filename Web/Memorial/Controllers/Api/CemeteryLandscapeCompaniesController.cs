using System;
using System.Collections.Generic;
using System.Web.Http;
using Memorial.Core.Dtos;
using Memorial.Lib.CemeteryLandscapeCompany;

namespace Memorial.Controllers.Api
{
    [RoutePrefix("api/cemeterylandscapeCompanies")]
    public class CemeteryLandscapeCompaniesController : ApiController
    {
        private readonly ICemeteryLandscapeCompany _cemeteryLandscapeCompany;

        public CemeteryLandscapeCompaniesController(ICemeteryLandscapeCompany cemeteryLandscapeCompany)
        {
            _cemeteryLandscapeCompany = cemeteryLandscapeCompany;
        }

        [Route("")]
        [HttpGet]
        public IEnumerable<CemeteryLandscapeCompanyDto> GetCemeteryLandscapeCompanyDtos()
        {
            return _cemeteryLandscapeCompany.GetCemeteryLandscapeCompanyDtos();
        }

        [Route("{id:int}")]
        [HttpGet]
        public CemeteryLandscapeCompanyDto GetCemeteryLandscapeCompanyDto(int id)
        {
            return _cemeteryLandscapeCompany.GetCemeteryLandscapeCompanyDto(id);
        }

        [Route("")]
        [HttpPost]
        public IHttpActionResult CreateFengShuiMaster(CemeteryLandscapeCompanyDto cemeteryLandscapeCompanyDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var id = _cemeteryLandscapeCompany.Create(cemeteryLandscapeCompanyDto);

            if (id == 0)
                return InternalServerError();

            cemeteryLandscapeCompanyDto.Id = id;

            return Created(new Uri(Request.RequestUri + "/" + id), cemeteryLandscapeCompanyDto);
        }

        [Route("{id:int}")]
        [HttpPut]
        public IHttpActionResult UpdateCemeteryLandscapeCompany(int id, CemeteryLandscapeCompanyDto cemeteryLandscapeCompanyDto)
        {
            if (_cemeteryLandscapeCompany.Update(cemeteryLandscapeCompanyDto))
                return Ok();
            else
                return InternalServerError();
        }

        [Route("{id:int}")]
        [HttpDelete]
        public IHttpActionResult DeleteCemeteryLandscapeCompany(int id)
        {
            if (_cemeteryLandscapeCompany.Delete(id))
                return Ok();
            else
                return InternalServerError();
        }

    }
}