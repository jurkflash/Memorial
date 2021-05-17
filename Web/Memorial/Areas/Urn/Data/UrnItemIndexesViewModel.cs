using System.Collections.Generic;
using Memorial.Core.Dtos;
using PagedList;

namespace Memorial.ViewModels
{
    public class UrnItemIndexesViewModel
    {
        public IPagedList<UrnTransactionDto> UrnTransactionDtos { get; set; }

        public int UrnItemId { get; set; }

        public int ApplicantId { get; set; }

        public bool AllowNew { get; set; }
    }
}