using System;
using System.Collections.Generic;
using System.Web.Http;
using Memorial.ViewModels;
using Memorial.Lib.Space;

namespace Memorial.Controllers.Api
{
    [RoutePrefix("api/spaceitems")]
    public class SpacesController : ApiController
    {
        private readonly ISpace _space;
        private readonly ITransaction _transaction;

        public SpacesController(ISpace space, ITransaction transaction)
        {
            _space = space;
            _transaction = transaction;
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

        
    }
}
