using Memorial.Core.Dtos;
using PagedList;

namespace Memorial.ViewModels
{
    public class ColumbariumItemIndexesViewModel
    {
        public IPagedList<ColumbariumTransactionDto> ColumbariumTransactionDtos { get; set; }

        public int ColumbariumItemId { get; set; }

        public NicheDto NicheDto { get; set; }

        public int NicheId { get; set; }

        public int ApplicantId { get; set; }

        public bool OrderFlag { get; set; }

        public string SystemCode { get; set; }

        public bool AllowNew { get; set; }
    }
}