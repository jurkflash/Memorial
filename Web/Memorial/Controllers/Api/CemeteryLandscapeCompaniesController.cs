using System;
using System.Collections.Generic;
using System.Web.Http;
using AutoMapper;
using Memorial.Core.Domain;
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
        public IEnumerable<CemeteryLandscapeCompanyDto> GetAll()
        {
            return Mapper.Map<IEnumerable<CemeteryLandscapeCompanyDto>>(_cemeteryLandscapeCompany.GetAll());
        }

        [Route("{id:int}")]
        [HttpGet]
        public CemeteryLandscapeCompanyDto Get(int id)
        {
            return Mapper.Map<CemeteryLandscapeCompanyDto>(_cemeteryLandscapeCompany.Get(id));
        }

        [Route("")]
        [HttpPost]
        public IHttpActionResult Add(CemeteryLandscapeCompanyDto cemeteryLandscapeCompanyDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var cemeteryLandscapeCompany = Mapper.Map<Core.Domain.CemeteryLandscapeCompany>(cemeteryLandscapeCompanyDto);
            var id = _cemeteryLandscapeCompany.Add(cemeteryLandscapeCompany);

            if (id == 0)
                return InternalServerError();

            cemeteryLandscapeCompanyDto.Id = id;

            return Created(new Uri(Request.RequestUri + "/" + id), cemeteryLandscapeCompanyDto);
        }

        [Route("{id:int}")]
        [HttpPut]
        public IHttpActionResult Change(int id, CemeteryLandscapeCompanyDto cemeteryLandscapeCompanyDto)
        {
            var cemeteryLandscapeCompany = Mapper.Map<Core.Domain.CemeteryLandscapeCompany>(cemeteryLandscapeCompanyDto);
            if (_cemeteryLandscapeCompany.Change(id, cemeteryLandscapeCompany))
                return Ok();
            else
                return InternalServerError();
        }

        [Route("{id:int}")]
        [HttpDelete]
        public IHttpActionResult Remove(int id)
        {
            if (_cemeteryLandscapeCompany.Remove(id))
                return Ok();
            else
                return InternalServerError();
        }

    }
}