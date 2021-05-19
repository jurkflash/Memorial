using System.Collections.Generic;
using Memorial.Core.Dtos;

namespace Memorial.ViewModels
{
    public class ColumbariumCentreIndexesViewModel
    {
        public IEnumerable<ColumbariumCentreDto> ColumbariumCentreDtos { get; set; }

        public int ApplicantId { get; set; }
    }
}