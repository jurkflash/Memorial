using System;
using System.Collections.Generic;
using System.Web.Http;
using AutoMapper;
using Memorial.Core.Dtos;
using Memorial.Lib.Columbarium;

namespace Memorial.Controllers.Api
{
    [RoutePrefix("api/columbariums")]
    public class ColumbariumNichesController : ApiController
    {
        private readonly INiche _niche;
        private readonly INicheType _nicheType;

        public ColumbariumNichesController(INiche niche, INicheType nicheType)
        {
            _niche = niche;
            _nicheType = nicheType;
        }

        [Route("types")]
        [HttpGet]
        public IEnumerable<NicheTypeDto> GetAll()
        {
            return Mapper.Map<IEnumerable<NicheTypeDto>>(_nicheType.GetAll());
        }

        [Route("areas/{areaId:int}/availableNiches")]
        [HttpGet]
        public IEnumerable<NicheDto> GetAvailableNicheByAreaId(int areaId)
        {
            return Mapper.Map<IEnumerable<NicheDto>>(_niche.GetAvailableNichesByAreaId(areaId));
        }

        [Route("areas/{areaId:int}/niches")]
        [HttpGet]
        public IHttpActionResult GetByAreaIdAndPositions(int areaId, int positionX, int positionY)
        {
            return Ok(Mapper.Map< NicheDto>(_niche.GetByAreaIdAndPostions(areaId, positionX, positionY)));
        }

        [Route("areas/{areaId:int}/types/{nicheTypeId:int}/niches")]
        [HttpGet]
        public IEnumerable<NicheDto> GetByAreaIdAndTypeId(int areaId, int nicheTypeId)
        {
            return Mapper.Map<IEnumerable<NicheDto>>(_niche.GetByAreaIdAndTypeId(areaId, nicheTypeId, null));
        }

        [Route("niches/{id:int}")]
        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            return Ok(Mapper.Map<NicheDto>(_niche.GetById(id)));
        }

        [Route("niches")]
        [HttpPost]
        public IHttpActionResult CreateNiche(NicheDto nicheDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var niche = Mapper.Map<Core.Domain.Niche>(nicheDto);
            var id = _niche.Add(niche);

            if (id == 0)
                return InternalServerError();

            nicheDto.Id = id;

            return Created(new Uri(Request.RequestUri + "/" + id), nicheDto);
        }

        [Route("niches/{id:int}")]
        [HttpPut]
        public IHttpActionResult UpdateNiche(int id, NicheDto nicheDto)
        {
            var niche = Mapper.Map<Core.Domain.Niche>(nicheDto);
            if (_niche.Change(id, niche))
                return Ok();
            else
                return InternalServerError();
        }

        [Route("niches/{id:int}")]
        [HttpDelete]
        public IHttpActionResult Remove(int id)
        {
            if (_niche.Remove(id))
                return Ok();
            else
                return InternalServerError();
        }

    }
}
