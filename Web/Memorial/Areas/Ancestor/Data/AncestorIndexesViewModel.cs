using System.Collections.Generic;
using Memorial.Core.Dtos;

namespace Memorial.ViewModels
{
    public class AncestorIndexesViewModel
    {
        public IEnumerable<AncestorDto> AncestorDtos { get; set; }

        public IDictionary<byte, IEnumerable<byte>> Positions { get; set; }

        public int ApplicantId { get; set; }
    }
}