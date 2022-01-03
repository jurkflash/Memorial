using System.Collections.Generic;
using Memorial.Core.Dtos;

namespace Memorial.ViewModels
{
    public class UrnIndexesViewModel
    {
        public IEnumerable<UrnDto> UrnDtos { get; set; }

        public int? ApplicantId { get; set; }
    }
}