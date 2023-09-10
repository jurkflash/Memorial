using System;
using System.Web.Http;
using System.Collections.Generic;
using Memorial.Core.Dtos;
using Memorial.Lib.Space;
using AutoMapper;

namespace Memorial.Controllers.Api
{
    [RoutePrefix("api/spaces/mains/items")]
    public class SpaceItemsController : ApiController
    {
        private readonly IItem _item;

        public SpaceItemsController(IItem item)
        {
            _item = item;
        }

        [Route("~/api/spaces/mains/{id:int}/items")]
        [HttpGet]
        public IEnumerable<SpaceItemDto> GetAlls(int id)
        {
            return Mapper.Map<IEnumerable<SpaceItemDto>>(_item.GetBySpace(id));
        }

        [Route("{id:int}")]
        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            return Ok(Mapper.Map<SpaceItemDto>(_item.GetById(id)));
        }

        [Route("~/api/spaces/mains/{id:int}/availableitems")]
        [HttpGet]
        public IEnumerable<SubProductServiceDto> GetAvailableItemsBySpace(int id)
        {
            return Mapper.Map<IEnumerable<SubProductServiceDto>>(_item.GetAvailableItemBySpace(id));
        }

        [Route("")]
        [HttpPost]
        public IHttpActionResult Add(SpaceItemDto itemDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var item = Mapper.Map<Core.Domain.SpaceItem>(itemDto);
            var id = _item.Add(item);

            if (id == 0)
                return InternalServerError();

            return Created(new Uri(Request.RequestUri + "/" + id), itemDto);
        }

        [Route("{id:int}")]
        [HttpPut]
        public IHttpActionResult Change(int id, SpaceItemDto itemDto)
        {
            var item = Mapper.Map<Core.Domain.SpaceItem>(itemDto);
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
