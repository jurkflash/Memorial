using System;
using System.Collections.Generic;
using System.Web.Http;
using Memorial.Core.Dtos;
using Memorial.Lib.FuneralCompany;

namespace Memorial.Controllers.Api
{
    [RoutePrefix("api/funeralcompanies")]
    public class FuneralCompaniesController : ApiController
    {
        private readonly IFuneralCompany _funeralCompany;

        public FuneralCompaniesController(IFuneralCompany funeralCompany)
        {
            _funeralCompany = funeralCompany;
        }

        [Route("")]
        [HttpGet]
        public IEnumerable<FuneralCompanyDto> GetUrnDtos()
        {
            return _funeralCompany.GetFuneralCompanyDtos();
        }

        [Route("{id:int}")]
        [HttpGet]
        public FuneralCompanyDto GetFuneralCompanyDto(int id)
        {
            return _funeralCompany.GetFuneralCompanyDto(id);
        }

        [Route("")]
        [HttpPost]
        public IHttpActionResult CreateFuneralCompany(FuneralCompanyDto funeralCompanyDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var id = _funeralCompany.Create(funeralCompanyDto);

            if (id == 0)
                return InternalServerError();

            funeralCompanyDto.Id = id;

            return Created(new Uri(Request.RequestUri + "/" + id), funeralCompanyDto);
        }

        [Route("{id:int}")]
        [HttpPut]
        public IHttpActionResult UpdateFuneralCompany(int id, FuneralCompanyDto funeralCompanyDto)
        {
            if (_funeralCompany.Update(funeralCompanyDto))
                return Ok();
            else
                return InternalServerError();
        }

        [Route("{id:int}")]
        [HttpDelete]
        public IHttpActionResult DeleteFuneralCompany(int id)
        {
            if (_funeralCompany.Delete(id))
                return Ok();
            else
                return InternalServerError();
        }

    }
}