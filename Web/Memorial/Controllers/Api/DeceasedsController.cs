using System.Collections.Generic;
using System.Web.Http;
using Memorial.Core.Dtos;
using AutoMapper;
using Memorial.Lib.Deceased;
using Memorial.Core.Domain;

namespace Memorial.Controllers.Api
{
    [Authorize]
    [RoutePrefix("api/deceaseds")]
    public class DeceasedsController : ApiController
    {
        private readonly IDeceased _deceased;

        public DeceasedsController(IDeceased deceased)
        {
            _deceased = deceased;
        }

        [Route("~/api/applicants/{applicantId:int}/deceaseds")]
        [HttpGet]
        public IEnumerable<DeceasedDto> GetByApplicantId(int applicantId)
        {
            var result = Mapper.Map<IEnumerable<DeceasedDto>>(_deceased.GetByApplicantId(applicantId));

            return result;
        }

        [Route("~/api/applicants/{applicantId:int}/unlinkeddeceaseds")]
        [HttpGet]
        public IEnumerable<DeceasedDto> GetUnlinkedDeceasedDtosByApplicantId(int applicantId)
        {
            var result = Mapper.Map<IEnumerable<DeceasedDto>>(_deceased.GetExcludeFilter(applicantId, null));

            return result;
        }

        [Route("{id:int}")]
        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            if(_deceased.IsRecordLinked(id))
                return BadRequest("Record linked");

            if(_deceased.Remove(id))
                return Ok();

            return BadRequest("Record linked");
        }
    }
}
