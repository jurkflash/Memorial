using System;
using System.Web.Http;
using System.Collections.Generic;
using Memorial.Core.Dtos;
using Memorial.Lib.Space;
using Memorial.ViewModels;
using AutoMapper;

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
        public IEnumerable<SpaceDto> GetBySite(int siteId)
        {
            return Mapper.Map<IEnumerable<SpaceDto>>(_space.GetBySite(siteId));
        }

        [Route("{id:int}")]
        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            return Ok(Mapper.Map<SpaceDto>(_space.Get(id)));
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
                    ItemId = t.SpaceItemId,
                    BackgroundColor = string.IsNullOrEmpty(t.SpaceColorCode) ? "#FFFFFF" : t.SpaceColorCode
                });
            }

            return Json(events.ToArray());
        }

        [Route("")]
        [HttpPost]
        public IHttpActionResult Add(SpaceDto spaceDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var space = Mapper.Map<Core.Domain.Space>(spaceDto);
            var id = _space.Add(space);

            if (id == 0)
                return InternalServerError();

            spaceDto.Id = id;

            return Created(new Uri(Request.RequestUri + "/" + id), spaceDto);
        }

        [Route("{id:int}")]
        [HttpPut]
        public IHttpActionResult Change(int id, SpaceDto spaceDto)
        {
            var space = Mapper.Map<Core.Domain.Space>(spaceDto);
            if (_space.Change(id, space))
                return Ok();
            else
                return InternalServerError();
        }

        [Route("{id:int}")]
        [HttpDelete]
        public IHttpActionResult Remove(int id)
        {
            if (_space.Remove(id))
                return Ok();
            else
                return InternalServerError();
        }
    }
}
