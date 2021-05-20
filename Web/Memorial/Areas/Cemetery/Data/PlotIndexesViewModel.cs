using System.Collections.Generic;
using Memorial.Core.Dtos;
using PagedList;

namespace Memorial.ViewModels
{
    public class PlotIndexesViewModel
    {
        public IPagedList<PlotDto> PlotDtos { get; set; }

        public IEnumerable<PlotTypeDto> PlotTypeDtos { get; set; }

        public int? SelectedPlotTypeId { get; set; }

        public int ApplicantId { get; set; }

        public int AreaId { get; set; }
    }
}