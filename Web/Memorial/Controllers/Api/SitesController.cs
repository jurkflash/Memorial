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
using Memorial.Lib.Site;

namespace Memorial.Controllers.Api
{
    public class SitesController : ApiController
    {
        private readonly ISite _site;

        public SitesController(ISite site)
        {
            _site = site;
        }

        public IHttpActionResult GetSites()
        {
            return Ok(_site.GetSiteDtos());
        }

    }
}
