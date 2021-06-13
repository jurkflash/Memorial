using System;
using System.Web.Http;
using Memorial.Core.Dtos;
using System.Collections.Generic;
using Memorial.Lib.Cemetery;

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
        public IEnumerable<PlotTypeDto> GetPlotTypeDtos()
        {
            return _plotType.GetPlotTypeDtos();
        }

        [Route("~/api/cemeteries/areas/{areaId:int}/types")]
        [HttpGet]
        public IEnumerable<PlotTypeDto> GetPlotTypeDtosByAreaId(int areaId)
        {
            return _plot.GetPlotTypeDtosByAreaId(areaId);
        }

        [Route("~/api/cemeteries/areas/{areaId:int}/types/{plotTypeId:int}/availableplots")]
        [HttpGet]
        public IEnumerable<PlotDto> GetAvailablePlotDtosByTypeIdAndAreaId(int areaId, int plotTypeId)
        {
            return _plot.GetAvailablePlotDtosByTypeIdAndAreaId(plotTypeId, areaId);
        }

        [Route("~/api/cemeteries/areas/{areaId:int}/types/{plotTypeId:int}/plots")]
        [HttpGet]
        public IEnumerable<PlotDto> GetPlotDtosByAreaIdAndTypeId(int areaId, int plotTypeId)
        {
            return _plot.GetPlotDtosByAreaIdAndTypeId(areaId, plotTypeId, null);
        }

        [Route("{id:int}")]
        [HttpGet]
        public IHttpActionResult GetPlotDto(int id)
        {
            return Ok(_plot.GetPlotDto(id));
        }

        [Route("")]
        [HttpPost]
        public IHttpActionResult CreatePlot(PlotDto plotDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var id = _plot.Create(plotDto);

            if (id == 0)
                return InternalServerError();

            plotDto.Id = id;

            return Created(new Uri(Request.RequestUri + "/" + id), plotDto);
        }

        [Route("{id:int}")]
        [HttpPut]
        public IHttpActionResult UpdatePlot(int id, PlotDto plotDto)
        {
            if (_plot.Update(plotDto))
                return Ok();
            else
                return InternalServerError();
        }

        [Route("{id:int}")]
        [HttpDelete]
        public IHttpActionResult DeletePlot(int id)
        {
            if (_plot.Delete(id))
                return Ok();
            else
                return InternalServerError();
        }

    }
}
