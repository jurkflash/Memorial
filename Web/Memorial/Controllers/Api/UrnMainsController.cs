using System;
using System.Web.Http;
using System.Collections.Generic;
using Memorial.Core.Dtos;
using Memorial.Lib.Urn;
using AutoMapper;

namespace Memorial.Controllers.Api
{
    [RoutePrefix("api/urns/mains")]
    public class UrnMainsController : ApiController
    {
        private readonly IUrn _urn;

        public UrnMainsController(IUrn urn)
        {
            _urn = urn;
        }

        [Route("~/api/sites/{siteId:int}/urns/mains")]
        [HttpGet]
        public IEnumerable<UrnDto> GetBySite(int siteId)
        {
            return Mapper.Map<IEnumerable<UrnDto>>(_urn.GetBySite(siteId));
        }

        [Route("{id:int}")]
        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            return Ok(Mapper.Map<UrnDto>(_urn.Get(id)));
        }

        [Route("")]
        [HttpPost]
        public IHttpActionResult Add(UrnDto urnDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var urn = Mapper.Map<Core.Domain.Urn>(urnDto);
            var id = _urn.Add(urn);

            if (id == 0)
                return InternalServerError();

            urnDto.Id = id;

            return Created(new Uri(Request.RequestUri + "/" + id), urnDto);
        }

        [Route("{id:int}")]
        [HttpPut]
        public IHttpActionResult Change(int id, UrnDto urnDto)
        {
            var urn = Mapper.Map<Core.Domain.Urn>(urnDto);
            if (_urn.Change(id, urn))
                return Ok();
            else
                return InternalServerError();
        }

        [Route("{id:int}")]
        [HttpDelete]
        public IHttpActionResult Remove(int id)
        {
            if (_urn.Remove(id))
                return Ok();
            else
                return InternalServerError();
        }
    }
}
