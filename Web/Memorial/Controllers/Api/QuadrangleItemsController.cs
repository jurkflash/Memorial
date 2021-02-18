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
    public class QuadrangleItemsController : ApiController
    {
        private readonly IQuadrangleItem _quadrangleItem;

        public QuadrangleItemsController(IQuadrangleItem quadrangleItem)
        {
            _quadrangleItem = quadrangleItem;
        }

        public IHttpActionResult GetAmount(DateTime from, DateTime to, int itemId)
        {
            if (from > to)
                return BadRequest();

            var result = _quadrangleItem.GetAmount(from, to, itemId);

            if (result == -1)
                return BadRequest();
            else
                return Ok(result);
        }

    }
}
