using System;
using System.Collections.Generic;
using System.Web.Http;
using AutoMapper;
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
        public IEnumerable<FengShuiMasterDto> GetAll()
        {
            return Mapper.Map<IEnumerable<FengShuiMasterDto>>(_fengShuiMaster.GetAll());
        }

        [Route("{id:int}")]
        [HttpGet]
        public FengShuiMasterDto Get(int id)
        {
            return Mapper.Map<FengShuiMasterDto>(_fengShuiMaster.Get(id));
        }

        [Route("")]
        [HttpPost]
        public IHttpActionResult Add(FengShuiMasterDto fengShuiMasterDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var fengShuiMaster = Mapper.Map<Core.Domain.FengShuiMaster>(fengShuiMasterDto);
            var id = _fengShuiMaster.Add(fengShuiMaster);

            if (id == 0)
                return InternalServerError();

            fengShuiMasterDto.Id = id;

            return Created(new Uri(Request.RequestUri + "/" + id), fengShuiMasterDto);
        }

        [Route("{id:int}")]
        [HttpPut]
        public IHttpActionResult Change(int id, FengShuiMasterDto fengShuiMasterDto)
        {
            var fengShuiMaster = Mapper.Map<Core.Domain.FengShuiMaster>(fengShuiMasterDto);
            if (_fengShuiMaster.Change(id, fengShuiMaster))
                return Ok();
            else
                return InternalServerError();
        }

        [Route("{id:int}")]
        [HttpDelete]
        public IHttpActionResult Remove(int id)
        {
            if (_fengShuiMaster.Remove(id))
                return Ok();
            else
                return InternalServerError();
        }

    }
}