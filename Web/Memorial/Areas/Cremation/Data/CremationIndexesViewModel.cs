using System.Collections.Generic;
using Memorial.Core.Dtos;

namespace Memorial.ViewModels
{
    public class CremationIndexesViewModel
    {
        public IEnumerable<CremationDto> CremationDtos { get; set; }

        public int ApplicantId { get; set; }
    }
}