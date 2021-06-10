using System;
using System.Web.Http;
using System.Collections.Generic;
using Memorial.Core.Dtos;
using Memorial.Lib.SubProductService;

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

        public IEnumerable<SubProductServiceDto> GetSubProductServices()
        {
            return _subProductService.GetSubProductServiceDtos();
        }

        public IHttpActionResult GetSubProductService(int id)
        {
            return Ok(_subProductService.GetSubProductServiceDto(id));
        }

    }
}
