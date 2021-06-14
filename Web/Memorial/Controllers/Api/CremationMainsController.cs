using System;
using System.Web.Http;
using System.Collections.Generic;
using Memorial.Core.Dtos;
using Memorial.Lib.Cremation;

namespace Memorial.Controllers.Api
{
    [RoutePrefix("api/cremations/mains")]
    public class CremationMainsController : ApiController
    {
        private readonly ICremation _cremation;

        public CremationMainsController(ICremation cremation)
        {
            _cremation = cremation;
        }

        [Route("~/api/sites/{siteId:int}/cremations/mains")]
        [HttpGet]
        public IEnumerable<CremationDto> GetCremationDtosBySite(int siteId)
        {
            return _cremation.GetCremationDtosBySite(siteId);
        }

        [Route("{id:int}")]
        [HttpGet]
        public IHttpActionResult GetCremationDto(int id)
        {
            return Ok(_cremation.GetCremationDto(id));
        }

        [Route("")]
        [HttpPost]
        public IHttpActionResult CreateCremation(CremationDto cremationDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var id = _cremation.Create(cremationDto);

            if (id == 0)
                return InternalServerError();

            cremationDto.Id = id;

            return Created(new Uri(Request.RequestUri + "/" + id), cremationDto);
        }

        [Route("{id:int}")]
        [HttpPut]
        public IHttpActionResult UpdateArea(int id, CremationDto cremationDto)
        {
            if (_cremation.Update(cremationDto))
                return Ok();
            else
                return InternalServerError();
        }

        [Route("{id:int}")]
        [HttpDelete]
        public IHttpActionResult DeleteArea(int id)
        {
            if (_cremation.Delete(id))
                return Ok();
            else
                return InternalServerError();
        }
    }
}
