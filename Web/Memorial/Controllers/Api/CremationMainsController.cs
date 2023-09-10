using System;
using System.Web.Http;
using System.Collections.Generic;
using Memorial.Core.Dtos;
using Memorial.Lib.Cremation;
using AutoMapper;

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
        public IEnumerable<CremationDto> GetBySite(int siteId)
        {
            return Mapper.Map<IEnumerable<CremationDto>>(_cremation.GetBySite(siteId));
        }

        [Route("{id:int}")]
        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            return Ok(Mapper.Map<CremationDto>(_cremation.GetById(id)));
        }

        [Route("")]
        [HttpPost]
        public IHttpActionResult Add(CremationDto cremationDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var cremation = Mapper.Map<Core.Domain.Cremation>(cremationDto);
            var id = _cremation.Add(cremation);

            if (id == 0)
                return InternalServerError();

            cremationDto.Id = id;

            return Created(new Uri(Request.RequestUri + "/" + id), cremationDto);
        }

        [Route("{id:int}")]
        [HttpPut]
        public IHttpActionResult Change(int id, CremationDto cremationDto)
        {
            var cremation = Mapper.Map<Core.Domain.Cremation>(cremationDto);
            if (_cremation.Change(id, cremation))
                return Ok();
            else
                return InternalServerError();
        }

        [Route("{id:int}")]
        [HttpDelete]
        public IHttpActionResult Remove(int id)
        {
            if (_cremation.Remove(id))
                return Ok();
            else
                return InternalServerError();
        }
    }
}
