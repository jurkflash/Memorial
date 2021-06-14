using System;
using System.Web.Http;
using System.Collections.Generic;
using Memorial.Core.Dtos;
using Memorial.Lib.Urn;

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
        public IEnumerable<UrnDto> GetUrnDtosBySite(int siteId)
        {
            return _urn.GetUrnDtosBySite(siteId);
        }

        [Route("{id:int}")]
        [HttpGet]
        public IHttpActionResult GetUrnDto(int id)
        {
            return Ok(_urn.GetUrnDto(id));
        }

        [Route("")]
        [HttpPost]
        public IHttpActionResult CreateUrn(UrnDto urnDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var id = _urn.Create(urnDto);

            if (id == 0)
                return InternalServerError();

            urnDto.Id = id;

            return Created(new Uri(Request.RequestUri + "/" + id), urnDto);
        }

        [Route("{id:int}")]
        [HttpPut]
        public IHttpActionResult UpdateUrn(int id, UrnDto urnDto)
        {
            if (_urn.Update(urnDto))
                return Ok();
            else
                return InternalServerError();
        }

        [Route("{id:int}")]
        [HttpDelete]
        public IHttpActionResult DeleteUrn(int id)
        {
            if (_urn.Delete(id))
                return Ok();
            else
                return InternalServerError();
        }
    }
}
