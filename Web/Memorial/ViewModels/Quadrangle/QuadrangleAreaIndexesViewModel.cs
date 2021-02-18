using System.Collections.Generic;
using Memorial.Core.Dtos;

namespace Memorial.ViewModels
{
    public class QuadrangleAreaIndexesViewModel
    {
        public IEnumerable<QuadrangleAreaDto> QuadrangleAreaDtos { get; set; }

        public int ApplicantId { get; set; }
    }
}