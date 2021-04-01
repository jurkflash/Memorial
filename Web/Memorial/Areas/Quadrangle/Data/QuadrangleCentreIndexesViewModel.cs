using System.Collections.Generic;
using Memorial.Core.Dtos;

namespace Memorial.ViewModels
{
    public class QuadrangleCentreIndexesViewModel
    {
        public IEnumerable<QuadrangleCentreDto> QuadrangleCentreDtos { get; set; }

        public int ApplicantId { get; set; }
    }
}