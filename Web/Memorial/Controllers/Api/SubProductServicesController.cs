using System;
using System.Web.Http;
using System.Collections.Generic;
using Memorial.Core.Dtos;
using Memorial.Lib.SubProductService;
using AutoMapper;

namespace Memorial.Controllers.Api
{
    [RoutePrefix("api/subproductservices")]
    public class SubProductServicesController : ApiController
    {
        private readonly ISubProductService _subProductService;

        public SubProductServicesController(ISubProductService subProductService)
        {
            _subProductService = subProductService;
        }

        public IEnumerable<SubProductServiceDto> GetAll()
        {
            return Mapper.Map<IEnumerable<SubProductServiceDto>>(_subProductService.GetAll());
        }

        public IHttpActionResult Get(int id)
        {
            return Ok(Mapper.Map<SubProductServiceDto>(_subProductService.Get(id)));
        }

    }
}
