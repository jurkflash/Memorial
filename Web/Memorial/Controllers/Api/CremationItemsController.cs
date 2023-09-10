using System;
using System.Web.Http;
using System.Collections.Generic;
using Memorial.Core.Dtos;
using Memorial.Lib.Cremation;
using AutoMapper;
using Memorial.Core.Domain;

namespace Memorial.Controllers.Api
{
    [RoutePrefix("api/cremations/mains/items")]
    public class CremationItemsController : ApiController
    {
        private readonly IItem _item;

        public CremationItemsController(IItem item)
        {
            _item = item;
        }

        [Route("~/api/cremations/mains/{id:int}/items")]
        [HttpGet]
        public IEnumerable<CremationItemDto> GetItems(int id)
        {
            return Mapper.Map<IEnumerable<CremationItemDto>>(_item.GetByCremation(id));
        }

        [Route("{id:int}")]
        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            return Ok(Mapper.Map<CremationItemDto>(_item.GetById(id)));
        }

        [Route("~/api/cremations/mains/{id:int}/availableitems")]
        [HttpGet]
        public IEnumerable<SubProductServiceDto> GetAvailableItemsByCremation(int id)
        {
            return Mapper.Map<IEnumerable<SubProductServiceDto>>(_item.GetAvailableItemByCremation(id));
        }

        [Route("")]
        [HttpPost]
        public IHttpActionResult Add(CremationItemDto itemDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var item = Mapper.Map<Core.Domain.CremationItem>(itemDto);
            var id = _item.Add(item);

            if (id == 0)
                return InternalServerError();

            return Created(new Uri(Request.RequestUri + "/" + id), itemDto);
        }

        [Route("{id:int}")]
        [HttpPut]
        public IHttpActionResult Change(int id, CremationItemDto itemDto)
        {
            var item = Mapper.Map<Core.Domain.CremationItem>(itemDto);
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
