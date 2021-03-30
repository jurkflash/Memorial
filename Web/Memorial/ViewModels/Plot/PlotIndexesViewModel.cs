using System.Collections.Generic;
using Memorial.Core.Dtos;

namespace Memorial.ViewModels
{
    public class PlotIndexesViewModel
    {
        public IEnumerable<PlotDto> PlotDtos { get; set; }

        public int ApplicantId { get; set; }
    }
}