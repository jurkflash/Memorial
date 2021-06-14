using System;
using System.Web.Http;
using System.Collections.Generic;
using Memorial.Core.Dtos;
using Memorial.Lib.Columbarium;

namespace Memorial.Controllers.Api
{
    [RoutePrefix("api/columbariums/centres/items")]
    public class ColumbariumItemsController : ApiController
    {
        private readonly IItem _item;

        public ColumbariumItemsController(IItem item)
        {
            _item = item;
        }

        [Route("~/api/columbariums/centres/{centreId:int}/items")]
        [HttpGet]
        public IEnumerable<ColumbariumItemDto> GetItems(int centreId)
        {
            return _item.GetItemDtosByCentre(centreId);
        }

        [Route("{id:int}")]
        [HttpGet]
        public IHttpActionResult GetItem(int id)
        {
            return Ok(_item.GetItemDto(id));
        }

        [Route("{itemId:int}/amount")]
        [HttpGet]
        public IHttpActionResult GetAmount(int itemId, DateTime from, DateTime to)
        {
            if (from > to)
                return BadRequest();

            var result = _item.GetAmountWithDateRange(itemId, from, to);

            if (result == -1)
                return BadRequest();
            else
                return Ok(result);
        }

        [Route("~/api/columbariums/centres/{centreId:int}/availableitems")]
        [HttpGet]
        public IEnumerable<SubProductServiceDto> GetAvailableItemsByCentre(int centreId)
        {
            return _item.GetAvailableItemDtosByCentre(centreId);
        }

        [Route("")]
        [HttpPost]
        public IHttpActionResult CreateItem(ColumbariumItemDto itemDto)
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
        public IHttpActionResult UpdateItem(int id, ColumbariumItemDto itemDto)
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
