using System;
using System.Web.Http;
using System.Collections.Generic;
using Memorial.Core.Dtos;
using Memorial.Lib.Miscellaneous;

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
        public IEnumerable<MiscellaneousDto> GetMiscellaneousDtosBySite(int siteId)
        {
            return _miscellaneous.GetMiscellaneousDtosBySite(siteId);
        }

        [Route("{id:int}")]
        [HttpGet]
        public IHttpActionResult GetMiscellaneousDto(int id)
        {
            return Ok(_miscellaneous.GetMiscellaneousDto(id));
        }

        [Route("")]
        [HttpPost]
        public IHttpActionResult CreateMiscellaneous(MiscellaneousDto miscellaneousDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var id = _miscellaneous.Create(miscellaneousDto);

            if (id == 0)
                return InternalServerError();

            miscellaneousDto.Id = id;

            return Created(new Uri(Request.RequestUri + "/" + id), miscellaneousDto);
        }

        [Route("{id:int}")]
        [HttpPut]
        public IHttpActionResult UpdateMiscellaneous(int id, MiscellaneousDto miscellaneousDto)
        {
            if (_miscellaneous.Update(miscellaneousDto))
                return Ok();
            else
                return InternalServerError();
        }

        [Route("{id:int}")]
        [HttpDelete]
        public IHttpActionResult DeleteMiscellaneous(int id)
        {
            if (_miscellaneous.Delete(id))
                return Ok();
            else
                return InternalServerError();
        }
    }
}
