using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Memorial.Core;
using Memorial.Core.Domain;
using Memorial.Core.Dtos;
using AutoMapper;
using Memorial.ViewModels;
using Memorial.Lib.Space;

namespace Memorial.Controllers.Api
{
    public class SpacesController : ApiController
    {
        private readonly ISpace _space;
        private readonly ITransaction _transaction;

        public SpacesController(ISpace space, ITransaction transaction)
        {
            _space = space;
            _transaction = transaction;
        }

        public IHttpActionResult GetAmount(DateTime from, DateTime to, int spaceItemId)
        {
            if (from > to)
                return BadRequest();

            var result = _space.GetAmount(from, to, spaceItemId);

            if (result == -1)
                return BadRequest();
            else
                return Ok(result);
        }

        public IHttpActionResult GetAvailability(DateTime from, DateTime to, int spaceItemId)
        {
            if (from > to)
                return BadRequest();

            var result = _space.CheckAvailability(from, to, spaceItemId);

            return Ok(result);
        }

        public IHttpActionResult GetAvailability(DateTime from, DateTime to, string AF)
        {
            if (from > to)
                return BadRequest();

            var result = _space.CheckAvailability(from, to, AF);

            return Ok(result);
        }

        public IHttpActionResult GetEvents(DateTime startDate, DateTime endDate, byte siteId)
        {
            var events = new List<SpaceCalendar>();
            var trx = _transaction.GetBookedTransaction(startDate, endDate, siteId);

            foreach(var t in trx)
            {
                events.Add(new SpaceCalendar()
                {
                    Title = t.SpaceName,
                    StartDate = t.FromDate.ToString("yyyy-MM-ddTHH:mm"),
                    EndDate = t.ToDate.ToString("yyyy-MM-ddTHH:mm"),
                    Desc = t.TransactionRemark,
                    AF = t.AF,
                    BackgroundColor = string.IsNullOrEmpty(t.SpaceColorCode) ? "#FFFFFF" : "#"+ t.SpaceColorCode
                });
            }

            return Json(events.ToArray());
        }
    }
}
