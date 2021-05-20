using System.Collections.Generic;
using Memorial.Core.Dtos;

namespace Memorial.ViewModels
{
    public class CemeteryAreaIndexesViewModel
    {
        public IEnumerable<CemeteryAreaDto> CemeteryAreaDtos { get; set; }

        public int ApplicantId { get; set; }
    }
}