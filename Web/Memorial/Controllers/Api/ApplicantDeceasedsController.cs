using System.Collections.Generic;
using System.Web.Http;
using AutoMapper;
using Memorial.Core.Dtos;
using Memorial.Lib.ApplicantDeceased;

namespace Memorial.Controllers.Api
{
    [RoutePrefix("api/applicantdeceaseds")]
    public class ApplicantDeceasedsController : ApiController
    {
        private readonly IApplicantDeceased _applicantDeceased;

        public ApplicantDeceasedsController(IApplicantDeceased applicantDeceased)
        {
            _applicantDeceased = applicantDeceased;
        }

        [Route("{applicantId:int}/flatten")]
        [HttpGet]
        public IHttpActionResult GetFlattenByApplicant(int applicantId)
        {
            var result = Mapper.Map<IEnumerable<ApplicantDeceasedFlattenDto>>(_applicantDeceased.GetApplicantDeceasedFlattensByApplicantId(applicantId));

            return Ok(result);
        }

        [Route("{id:int}")]
        [HttpDelete]
        public IHttpActionResult Remove(int id)
        {
            var result = _applicantDeceased.Remove(id);

            return Ok(result);
        }

        [Route("")]
        [HttpPost]
        public IHttpActionResult Add(ApplicantDeceasedDto applicantDeceasedDto)
        {
            var result = _applicantDeceased.Add(applicantDeceasedDto.ApplicantDtoId, applicantDeceasedDto.DeceasedDtoId, applicantDeceasedDto.RelationshipTypeDtoId);

            return Ok(result);
        }

    }
}
