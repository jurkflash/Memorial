using System;
using System.Web.Http;
using Memorial.Core.Dtos;
using System.Collections.Generic;
using Memorial.Lib.Cemetery;
using AutoMapper;

namespace Memorial.Controllers.Api
{
    [RoutePrefix("api/cemeteries/plots")]
    public class PlotsController : ApiController
    {
        private readonly IPlot _plot;
        private readonly IPlotType _plotType;

        public PlotsController(IPlot plot, IPlotType plotType)
        {
            _plot = plot;
            _plotType = plotType;
        }

        [Route("~/api/cemeteries/types")]
        [HttpGet]
        public IEnumerable<PlotTypeDto> GetAll()
        {
            return Mapper.Map<IEnumerable<PlotTypeDto>>(_plotType.GetAll());
        }

        [Route("~/api/cemeteries/areas/{areaId:int}/types")]
        [HttpGet]
        public IEnumerable<PlotTypeDto> GetPlotTypesByAreaId(int areaId)
        {
            return Mapper.Map<IEnumerable<PlotTypeDto>>(_plot.GetPlotTypesByAreaId(areaId));
        }

        [Route("~/api/cemeteries/areas/{areaId:int}/types/{plotTypeId:int}/availableplots")]
        [HttpGet]
        public IEnumerable<PlotDto> GetAvailablePlotDtosByTypeIdAndAreaId(int areaId, int plotTypeId)
        {
            return Mapper.Map<IEnumerable<PlotDto>>(_plot.GetAvailableByTypeAndArea(plotTypeId, areaId));
        }

        [Route("~/api/cemeteries/areas/{areaId:int}/types/{plotTypeId:int}/plots")]
        [HttpGet]
        public IEnumerable<PlotDto> GetByAreaIdAndTypeId(int areaId, int plotTypeId)
        {
            return Mapper.Map<IEnumerable<PlotDto>>(_plot.GetByAreaIdAndTypeId(areaId, plotTypeId, null));
        }

        [Route("{id:int}")]
        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            return Ok(Mapper.Map<PlotDto>(_plot.GetById(id)));
        }

        [Route("")]
        [HttpPost]
        public IHttpActionResult Add(PlotDto plotDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var plot = Mapper.Map<Core.Domain.Plot>(plotDto);
            var id = _plot.Add(plot);

            if (id == 0)
                return InternalServerError();

            plotDto.Id = id;

            return Created(new Uri(Request.RequestUri + "/" + id), plotDto);
        }

        [Route("{id:int}")]
        [HttpPut]
        public IHttpActionResult Change(int id, PlotDto plotDto)
        {
            var plot = Mapper.Map<Core.Domain.Plot>(plotDto);
            if (_plot.Change(id, plot))
                return Ok();
            else
                return InternalServerError();
        }

        [Route("{id:int}")]
        [HttpDelete]
        public IHttpActionResult Remove(int id)
        {
            if (_plot.Remove(id))
                return Ok();
            else
                return InternalServerError();
        }

    }
}
