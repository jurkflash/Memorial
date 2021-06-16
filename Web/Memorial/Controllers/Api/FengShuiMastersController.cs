using System;
using System.Collections.Generic;
using System.Web.Http;
using Memorial.Core.Dtos;
using Memorial.Lib.FengShuiMaster;

namespace Memorial.Controllers.Api
{
    [RoutePrefix("api/fengshuimasters")]
    public class FengShuiMastersController : ApiController
    {
        private readonly IFengShuiMaster _fengShuiMaster;

        public FengShuiMastersController(IFengShuiMaster fengShuiMaster)
        {
            _fengShuiMaster = fengShuiMaster;
        }

        [Route("")]
        [HttpGet]
        public IEnumerable<FengShuiMasterDto> GetFengShuiMasterDtos()
        {
            return _fengShuiMaster.GetFengShuiMasterDtos();
        }

        [Route("{id:int}")]
        [HttpGet]
        public FengShuiMasterDto GetFuneralCompanyDto(int id)
        {
            return _fengShuiMaster.GetFengShuiMasterDto(id);
        }

        [Route("")]
        [HttpPost]
        public IHttpActionResult CreateFengShuiMaster(FengShuiMasterDto fengShuiMasterDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var id = _fengShuiMaster.Create(fengShuiMasterDto);

            if (id == 0)
                return InternalServerError();

            fengShuiMasterDto.Id = id;

            return Created(new Uri(Request.RequestUri + "/" + id), fengShuiMasterDto);
        }

        [Route("{id:int}")]
        [HttpPut]
        public IHttpActionResult UpdateFengShuiMaster(int id, FengShuiMasterDto fengShuiMasterDto)
        {
            if (_fengShuiMaster.Update(fengShuiMasterDto))
                return Ok();
            else
                return InternalServerError();
        }

        [Route("{id:int}")]
        [HttpDelete]
        public IHttpActionResult DeleteFengShuiMaster(int id)
        {
            if (_fengShuiMaster.Delete(id))
                return Ok();
            else
                return InternalServerError();
        }

    }
}