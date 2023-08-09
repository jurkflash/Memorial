using System;
using System.Collections.Generic;
using System.Web.Http;
using AutoMapper;
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
        public IEnumerable<FuneralCompanyDto> GetAll()
        {
            return Mapper.Map<IEnumerable<FuneralCompanyDto>>(_funeralCompany.GetAll());
        }

        [Route("{id:int}")]
        [HttpGet]
        public FuneralCompanyDto Get(int id)
        {
            return Mapper.Map<FuneralCompanyDto>(_funeralCompany.Get(id));
        }

        [Route("")]
        [HttpPost]
        public IHttpActionResult Add(FuneralCompanyDto funeralCompanyDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var funeralCompany = Mapper.Map<Core.Domain.FuneralCompany>(funeralCompanyDto);
            var id = _funeralCompany.Add(funeralCompany);

            if (id == 0)
                return InternalServerError();

            funeralCompanyDto.Id = id;

            return Created(new Uri(Request.RequestUri + "/" + id), funeralCompanyDto);
        }

        [Route("{id:int}")]
        [HttpPut]
        public IHttpActionResult Change(int id, FuneralCompanyDto funeralCompanyDto)
        {
            var funeralCompany = Mapper.Map<Core.Domain.FuneralCompany>(funeralCompanyDto);
            if (_funeralCompany.Change(id, funeralCompany))
                return Ok();
            else
                return InternalServerError();
        }

        [Route("{id:int}")]
        [HttpDelete]
        public IHttpActionResult Remove(int id)
        {
            if (_funeralCompany.Remove(id))
                return Ok();
            else
                return InternalServerError();
        }

    }
}