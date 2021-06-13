using System.Web.Http;
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
            var result = _applicantDeceased.GetApplicantDeceasedFlattenDtosByApplicantId(applicantId);

            return Ok(result);
        }

        [Route("{id:int}")]
        [HttpDelete]
        public IHttpActionResult DeleteWithReturnDeceasedId(int id)
        {
            _applicantDeceased.SetApplicantDeceased(id);
            var result = _applicantDeceased.DeleteWithReturnDeceasedId();

            return Ok(result);
        }

        [Route("")]
        [HttpPost]
        public IHttpActionResult CreateWithReturnId(ApplicantDeceasedDto applicantDeceasedDto)
        {
            var result = _applicantDeceased.CreateWithReturnId(applicantDeceasedDto.ApplicantDtoId, applicantDeceasedDto.DeceasedDtoId, applicantDeceasedDto.RelationshipTypeDtoId);

            return Ok(result);
        }

    }
}
