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
using Memorial.Lib.Quadrangle;

namespace Memorial.Controllers.Api.Quadrangle
{
    public class ManagesController : ApiController
    {
        private readonly IManage _manage;

        public ManagesController(IManage manage)
        {
            _manage = manage;
        }

        public IHttpActionResult GetAmount(DateTime from, DateTime to)
        {
            if (from > to)
                return BadRequest();

            var result = _manage.GetAmount(from, to);

            if (result == -1)
                return BadRequest();
            else
                return Ok(result);
        }

    }
}
