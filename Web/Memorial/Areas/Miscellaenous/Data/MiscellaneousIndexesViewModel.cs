using System.Collections.Generic;
using Memorial.Core.Dtos;

namespace Memorial.ViewModels
{
    public class MiscellaneousIndexesViewModel
    {
        public IEnumerable<MiscellaneousDto> MiscellaneousDtos { get; set; }
        public int? ApplicantId { get; set; }
    }
}