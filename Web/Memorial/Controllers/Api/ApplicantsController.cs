using System.Web.Http;
using Memorial.Core.Dtos;
using Memorial.Lib.Applicant;

namespace Memorial.Controllers.Api
{
    [Authorize]
    [RoutePrefix("api/applicants")]
    public class ApplicantsController : ApiController
    {
        private readonly IApplicant _applicant;

        public ApplicantsController(IApplicant applicant)
        {
            _applicant = applicant;
        }

        [Route("{id:int}")]
        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            if (_applicant.IsRecordLinked(id))
                return BadRequest("Record linked");

            if (_applicant.Remove(id))
                return Ok();

            return BadRequest("Record linked");
        }

    }
}
