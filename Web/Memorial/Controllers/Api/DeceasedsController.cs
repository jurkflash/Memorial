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
using Memorial.Lib.Applicant;
using Memorial.Lib.Deceased;
using Memorial.Lib.ApplicantDeceased;

namespace Memorial.Controllers.Api
{
    public class DeceasedsController : ApiController
    {
        private readonly IDeceased _deceased;

        public DeceasedsController(IDeceased deceased)
        {
            _deceased = deceased;
        }

        public IHttpActionResult GetDeceasedsByApplicantId(int applicantId)
        {
            var result = _deceased.GetDeceasedsByApplicantId(applicantId);

            return Ok(Mapper.Map<IEnumerable<Core.Domain.Deceased>, IEnumerable<DeceasedDto>>(result));
        }

        public IHttpActionResult GetDeceasedsExcludeFilter(int applicantId, string deceasedName = null)
        {
            var result = _deceased.GetDeceasedsExcludeFilter(applicantId, deceasedName);

            return Ok(Mapper.Map<IEnumerable<Core.Domain.Deceased>, IEnumerable<DeceasedDto>>(result));
        }

    }
}
