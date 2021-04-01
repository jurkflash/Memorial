using System.Collections.Generic;
using Memorial.Core.Dtos;

namespace Memorial.ViewModels
{
    public class CremationItemsViewModel
    {
        public IEnumerable<CremationItemDto> CremationItemDtos { get; set; }

        public int ApplicantId { get; set; }
    }
}