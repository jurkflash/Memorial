using System.Collections.Generic;
using Memorial.Core.Dtos;

namespace Memorial.ViewModels
{
    public class AncestorAreaIndexesViewModel
    {
        public IEnumerable<AncestorAreaDto> AncestorAreaDtos { get; set; }

        public int ApplicantId { get; set; }
    }
}