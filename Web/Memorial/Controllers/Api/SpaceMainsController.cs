using System;
using System.Web.Http;
using System.Collections.Generic;
using Memorial.Core.Dtos;
using Memorial.Lib.Space;
using Memorial.ViewModels;

namespace Memorial.Controllers.Api
{
    [RoutePrefix("api/spaces/mains")]
    public class SpaceMainsController : ApiController
    {
        private readonly ISpace _space;
        private readonly ITransaction _transaction;

        public SpaceMainsController(ISpace space, ITransaction transaction)
        {
            _space = space;
            _transaction = transaction;
        }

        [Route("~/api/sites/{siteId:int}/spaces/mains")]
        [HttpGet]
        public IEnumerable<SpaceDto> GetSpaceDtosBySite(int siteId)
        {
            return _space.GetSpaceDtosBySite(siteId);
        }

        [Route("{id:int}")]
        [HttpGet]
        public IHttpActionResult GetSpaceDto(int id)
        {
            return Ok(_space.GetSpaceDto(id));
        }

        [Route("{id:int}/amount")]
        [HttpGet]
        public IHttpActionResult GetAmount(int id, DateTime from, DateTime to)
        {
            if (from > to)
                return BadRequest();

            var result = _space.GetAmount(from, to, id);

            if (result == -1)
                return BadRequest();
            else
                return Ok(result);
        }

        [Route("{id:int}/availability")]
        [HttpGet]
        public IHttpActionResult GetAvailability(int id, DateTime from, DateTime to)
        {
            if (from > to)
                return BadRequest();

            var result = _space.CheckAvailability(from, to, id);

            return Ok(result);
        }

        [Route("{AF}/availability")]
        [HttpGet]
        public IHttpActionResult GetAvailability(string AF, DateTime from, DateTime to)
        {
            if (from > to)
                return BadRequest();

            var result = _space.CheckAvailability(from, to, AF);

            return Ok(result);
        }

        [Route("~/api/sites/{siteId:int}/spaces/events")]
        [HttpGet]
        public IHttpActionResult GetEvents(int siteId, DateTime from, DateTime to)
        {
            var events = new List<SpaceCalendar>();
            var trx = _transaction.GetBookedTransaction(from, to, siteId);

            foreach (var t in trx)
            {
                events.Add(new SpaceCalendar()
                {
                    Title = t.SpaceName,
                    StartDate = t.FromDate.ToString("yyyy-MM-ddTHH:mm"),
                    EndDate = t.ToDate.ToString("yyyy-MM-ddTHH:mm"),
                    Desc = t.TransactionRemark,
                    AF = t.AF,
                    BackgroundColor = string.IsNullOrEmpty(t.SpaceColorCode) ? "#FFFFFF" : t.SpaceColorCode
                });
            }

            return Json(events.ToArray());
        }

        [Route("")]
        [HttpPost]
        public IHttpActionResult CreateSpace(SpaceDto spaceDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var id = _space.Create(spaceDto);

            if (id == 0)
                return InternalServerError();

            spaceDto.Id = id;

            return Created(new Uri(Request.RequestUri + "/" + id), spaceDto);
        }

        [Route("{id:int}")]
        [HttpPut]
        public IHttpActionResult UpdateSpace(int id, SpaceDto spaceDto)
        {
            if (_space.Update(spaceDto))
                return Ok();
            else
                return InternalServerError();
        }

        [Route("{id:int}")]
        [HttpDelete]
        public IHttpActionResult DeleteSpace(int id)
        {
            if (_space.Delete(id))
                return Ok();
            else
                return InternalServerError();
        }
    }
}
