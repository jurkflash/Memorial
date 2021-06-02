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
    public class ApplicantDeceasedsController : ApiController
    {
        private readonly IApplicantDeceased _applicantDeceased;

        public ApplicantDeceasedsController(IApplicantDeceased applicantDeceased)
        {
            _applicantDeceased = applicantDeceased;
        }

        public IHttpActionResult GetFlattenByApplicant(int applicantId)
        {
            var result = _applicantDeceased.GetApplicantDeceasedFlattenDtosByApplicantId(applicantId);

            return Ok(result);
        }

        [HttpDelete]
        public IHttpActionResult DeleteWithReturnDeceasedId(int id)
        {
            _applicantDeceased.SetApplicantDeceased(id);
            var result = _applicantDeceased.DeleteWithReturnDeceasedId();

            return Ok(result);
        }

        [HttpPost]
        public IHttpActionResult CreateWithReturnId(ApplicantDeceasedDto applicantDeceasedDto)
        {
            var result = _applicantDeceased.CreateWithReturnId(applicantDeceasedDto.ApplicantDtoId, applicantDeceasedDto.DeceasedDtoId, applicantDeceasedDto.RelationshipTypeDtoId);

            return Ok(result);
        }

    }
}
