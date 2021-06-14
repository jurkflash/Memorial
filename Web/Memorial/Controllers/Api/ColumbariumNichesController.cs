using System;
using System.Collections.Generic;
using System.Web.Http;
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
        public IEnumerable<NicheTypeDto> GetNicheTypeDtos()
        {
            return _nicheType.GetNicheTypeDtos();
        }

        [Route("areas/{areaId:int}/availableNiches")]
        [HttpGet]
        public IEnumerable<NicheDto> GetAvailableNicheDtosByAreaId(int areaId)
        {
            return _niche.GetAvailableNicheDtosByAreaId(areaId);
        }

        [Route("areas/{areaId:int}/niches")]
        [HttpGet]
        public IHttpActionResult GetNicheDtoByAreaIdAndPositions(int areaId, int positionX, int positionY)
        {
            return Ok(_niche.GetNicheDtoByAreaIdAndPostions(areaId, positionX, positionY));
        }

        [Route("areas/{areaId:int}/types/{nicheTypeId:int}/niches")]
        [HttpGet]
        public IEnumerable<NicheDto> GetNicheDtosByAreaIdAndTypeId(int areaId, int nicheTypeId)
        {
            return _niche.GetNicheDtosByAreaIdAndTypeId(areaId, nicheTypeId, null);
        }

        [Route("niches/{id:int}")]
        [HttpGet]
        public IHttpActionResult GetNicheDto(int id)
        {
            return Ok(_niche.GetNicheDto(id));
        }

        [Route("niches")]
        [HttpPost]
        public IHttpActionResult CreateNiche(NicheDto nicheDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var id = _niche.Create(nicheDto);

            if (id == 0)
                return InternalServerError();

            nicheDto.Id = id;

            return Created(new Uri(Request.RequestUri + "/" + id), nicheDto);
        }

        [Route("niches/{id:int}")]
        [HttpPut]
        public IHttpActionResult UpdateNiche(int id, NicheDto nicheDto)
        {
            if (_niche.Update(nicheDto))
                return Ok();
            else
                return InternalServerError();
        }

        [Route("niches/{id:int}")]
        [HttpDelete]
        public IHttpActionResult DeleteNiche(int id)
        {
            if (_niche.Delete(id))
                return Ok();
            else
                return InternalServerError();
        }

    }
}
