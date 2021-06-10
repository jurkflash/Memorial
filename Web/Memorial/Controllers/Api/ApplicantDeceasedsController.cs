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
