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
using Memorial.Lib;

namespace Memorial.Controllers.Api
{
    public class SpacesController : ApiController
    {
        private readonly ISpace _space;
        private readonly ISpaceTransaction _spaceTransaction;

        public SpacesController(ISpace space, ISpaceTransaction spaceTransaction)
        {
            _space = space;
            _spaceTransaction = spaceTransaction;
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

            //_spaceTransaction.GetTransaction("JST/SHALC/AF-00003/2021");
            //_spaceTransaction.UpdateModifyTime();

            return Ok(result);
        }
    }
}
