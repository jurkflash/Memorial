using System.Collections.Generic;
using Memorial.Core.Dtos;

namespace Memorial.ViewModels
{
    public class QuadrangleCentreIndexesViewModel
    {
        public IEnumerable<ColumbariumCentreDto> QuadrangleCentreDtos { get; set; }

        public int ApplicantId { get; set; }
    }
}