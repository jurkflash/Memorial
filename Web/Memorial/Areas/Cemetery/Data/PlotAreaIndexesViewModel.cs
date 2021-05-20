using System.Collections.Generic;
using Memorial.Core.Dtos;

namespace Memorial.ViewModels
{
    public class PlotAreaIndexesViewModel
    {
        public IEnumerable<PlotAreaDto> PlotAreaDtos { get; set; }

        public int ApplicantId { get; set; }
    }
}