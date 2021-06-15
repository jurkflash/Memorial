using System;
using System.Web.Http;
using System.Collections.Generic;
using Memorial.Core.Dtos;
using Memorial.Lib.Space;

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
        public IEnumerable<SpaceItemDto> GetItems(int id)
        {
            return _item.GetItemDtosBySpace(id);
        }

        [Route("{id:int}")]
        [HttpGet]
        public IHttpActionResult GetItem(int id)
        {
            return Ok(_item.GetItemDto(id));
        }

        [Route("~/api/spaces/mains/{id:int}/availableitems")]
        [HttpGet]
        public IEnumerable<SubProductServiceDto> GetAvailableItemsBySpace(int id)
        {
            return _item.GetAvailableItemDtosBySpace(id);
        }

        [Route("")]
        [HttpPost]
        public IHttpActionResult CreateItem(SpaceItemDto itemDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var id = _item.Create(itemDto);

            if (id == 0)
                return InternalServerError();

            return Created(new Uri(Request.RequestUri + "/" + id), itemDto);
        }

        [Route("{id:int}")]
        [HttpPut]
        public IHttpActionResult UpdateItem(int id, SpaceItemDto itemDto)
        {
            if (_item.Update(itemDto))
                return Ok();
            else
                return InternalServerError();
        }

        [Route("{id:int}")]
        [HttpDelete]
        public IHttpActionResult DeleteItem(int id)
        {
            if (_item.Delete(id))
                return Ok();
            else
                return InternalServerError();
        }
    }
}
