using System.Collections.Generic;
using Memorial.Core.Dtos;

namespace Memorial.ViewModels
{
    public class NicheIndexesViewModel
    {
        public IEnumerable<NicheDto> NicheDtos { get; set; }

        public IDictionary<byte, IEnumerable<byte>> Positions { get; set; }

        public int ApplicantId { get; set; }
    }
}