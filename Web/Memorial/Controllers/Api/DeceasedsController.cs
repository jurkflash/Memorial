using System.Collections.Generic;
using System.Web.Http;
using Memorial.Core.Dtos;
using AutoMapper;
using Memorial.Lib.Deceased;

namespace Memorial.Controllers.Api
{
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
        public IEnumerable<DeceasedDto> GetDeceasedsByApplicantId(int applicantId)
        {
            var result = _deceased.GetDeceasedDtosByApplicantId(applicantId);

            return result;
        }

        [Route("~/api/applicants/{applicantId:int}/unlinkeddeceaseds")]
        [HttpGet]
        public IEnumerable<DeceasedDto> GetUnlinkedDeceasedDtosByApplicantId(int applicantId)
        {
            var result = _deceased.GetDeceasedDtosExcludeFilter(applicantId, null);

            return result;
        }
        
    }
}
