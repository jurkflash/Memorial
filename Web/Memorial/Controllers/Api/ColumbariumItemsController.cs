using System;
using System.Web.Http;
using System.Collections.Generic;
using Memorial.Core.Dtos;
using Memorial.Lib.Columbarium;
using AutoMapper;

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
        public IEnumerable<ColumbariumItemDto> GetByCentre(int centreId)
        {
            return Mapper.Map<IEnumerable<ColumbariumItemDto>>(_item.GetByCentre(centreId));
        }

        [Route("{id:int}")]
        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            return Ok(Mapper.Map<ColumbariumItemDto>(_item.GetById(id)));
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
            return Mapper.Map<IEnumerable<SubProductServiceDto>>(_item.GetAvailableItemByCentre(centreId));
        }

        [Route("")]
        [HttpPost]
        public IHttpActionResult Add(ColumbariumItemDto itemDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var item = Mapper.Map<Core.Domain.ColumbariumItem>(itemDto);
            var id = _item.Add(item);

            if (id == 0)
                return InternalServerError();

            return Created(new Uri(Request.RequestUri + "/" + id), itemDto);
        }

        [Route("{id:int}")]
        [HttpPut]
        public IHttpActionResult Change(int id, ColumbariumItemDto itemDto)
        {
            var item = Mapper.Map<Core.Domain.ColumbariumItem>(itemDto);
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
