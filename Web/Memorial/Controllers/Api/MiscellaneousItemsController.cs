using System;
using System.Web.Http;
using System.Collections.Generic;
using Memorial.Core.Dtos;
using Memorial.Lib.Miscellaneous;

namespace Memorial.Controllers.Api
{
    [RoutePrefix("api/miscellaneous/mains/items")]
    public class MiscellaneousItemsController : ApiController
    {
        private readonly IItem _item;

        public MiscellaneousItemsController(IItem item)
        {
            _item = item;
        }

        [Route("~/api/miscellaneous/mains/{id:int}/items")]
        [HttpGet]
        public IEnumerable<MiscellaneousItemDto> GetItems(int id)
        {
            return _item.GetItemDtosByMiscellaneous(id);
        }

        [Route("{id:int}")]
        [HttpGet]
        public IHttpActionResult GetItem(int id)
        {
            return Ok(_item.GetItemDto(id));
        }

        [Route("~/api/miscellaneous/mains/{id:int}/availableitems")]
        [HttpGet]
        public IEnumerable<SubProductServiceDto> GetAvailableItemsByMiscellaneous(int id)
        {
            return _item.GetAvailableItemDtosByMiscellaneous(id);
        }

        [Route("")]
        [HttpPost]
        public IHttpActionResult CreateItem(MiscellaneousItemDto itemDto)
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
        public IHttpActionResult UpdateItem(int id, MiscellaneousItemDto itemDto)
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
