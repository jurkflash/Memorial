using System.Collections.Generic;
using Memorial.Core.Dtos;

namespace Memorial.ViewModels
{
    public class ColumbariumAreaIndexesViewModel
    {
        public IEnumerable<ColumbariumAreaDto> ColumbariumAreaDtos { get; set; }

        public int? ApplicantId { get; set; }

        public ColumbariumCentreDto ColumbariumCentreDto { get; set; }
    }
}