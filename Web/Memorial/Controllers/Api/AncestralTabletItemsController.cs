using System;
using System.Web.Http;
using System.Collections.Generic;
using Memorial.Core.Dtos;
using Memorial.Lib.AncestralTablet;
using AutoMapper;
using Memorial.Core.Domain;

namespace Memorial.Controllers.Api
{
    [RoutePrefix("api/ancestraltablets/areas/items")]
    public class AncestralTabletItemsController : ApiController
    {
        private readonly IItem _item;

        public AncestralTabletItemsController(IItem item)
        {
            _item = item;
        }

        [Route("~/api/ancestraltablets/areas/{areaId:int}/items")]
        [HttpGet]
        public IEnumerable<AncestralTabletItemDto> GetByArea(int areaId)
        {
            return Mapper.Map<IEnumerable<AncestralTabletItemDto>>(_item.GetByArea(areaId));
        }

        [Route("{id:int}")]
        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            return Ok(Mapper.Map<AncestralTabletItemDto>(_item.GetById(id)));
        }

        [Route("~/api/ancestraltablets/areas/{areaId:int}/availableitems")]
        [HttpGet]
        public IEnumerable<SubProductServiceDto> GetAvailableItemsByArea(int areaId)
        {
            return Mapper.Map<IEnumerable<SubProductServiceDto>>(_item.GetAvailableItemByArea(areaId));
        }

        [Route("")]
        [HttpPost]
        public IHttpActionResult Add(AncestralTabletItemDto itemDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var item = Mapper.Map<Core.Domain.AncestralTabletItem>(itemDto);
            var id = _item.Add(item);

            if (id == 0)
                return InternalServerError();

            return Created(new Uri(Request.RequestUri + "/" + id), itemDto);
        }

        [Route("{id:int}")]
        [HttpPut]
        public IHttpActionResult Change(int id, AncestralTabletItemDto itemDto)
        {
            var item = Mapper.Map<Core.Domain.AncestralTabletItem>(itemDto);
            if (_item.Change(id, item))
                return Ok();
            else
                return InternalServerError();
        }

        [Route("{id:int}")]
        [HttpDelete]
        public IHttpActionResult Remove(int id)
        {
            if (_item.Remove(id))
                return Ok();
            else
                return InternalServerError();
        }
    }
}
