using System;
using System.Web.Http;
using System.Collections.Generic;
using Memorial.Core.Dtos;
using Memorial.Lib.Miscellaneous;
using AutoMapper;

namespace Memorial.Controllers.Api
{
    [RoutePrefix("api/miscellaneous/mains")]
    public class MiscellaneousMainsController : ApiController
    {
        private readonly IMiscellaneous _miscellaneous;

        public MiscellaneousMainsController(IMiscellaneous miscellaneous)
        {
            _miscellaneous = miscellaneous;
        }

        [Route("~/api/sites/{siteId:int}/miscellaneous/mains")]
        [HttpGet]
        public IEnumerable<MiscellaneousDto> GetBySite(int siteId)
        {
            return Mapper.Map<IEnumerable<MiscellaneousDto>>(_miscellaneous.GetBySite(siteId));
        }

        [Route("{id:int}")]
        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            return Ok(Mapper.Map<MiscellaneousDto>(_miscellaneous.Get(id)));
        }

        [Route("")]
        [HttpPost]
        public IHttpActionResult Add(MiscellaneousDto miscellaneousDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var miscellaneous = Mapper.Map<Core.Domain.Miscellaneous>(miscellaneousDto);
            var id = _miscellaneous.Add(miscellaneous);

            if (id == 0)
                return InternalServerError();

            miscellaneousDto.Id = id;

            return Created(new Uri(Request.RequestUri + "/" + id), miscellaneousDto);
        }

        [Route("{id:int}")]
        [HttpPut]
        public IHttpActionResult Change(int id, MiscellaneousDto miscellaneousDto)
        {
            var miscellaneous = Mapper.Map<Core.Domain.Miscellaneous>(miscellaneousDto);
            if (_miscellaneous.Change(id, miscellaneous))
                return Ok();
            else
                return InternalServerError();
        }

        [Route("{id:int}")]
        [HttpDelete]
        public IHttpActionResult Remove(int id)
        {
            if (_miscellaneous.Remove(id))
                return Ok();
            else
                return InternalServerError();
        }
    }
}
