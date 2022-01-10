using Memorial.Core.Dtos;
using PagedList;

namespace Memorial.ViewModels
{
    public class SpaceItemIndexesViewModel
    {
        public IPagedList<SpaceTransactionDto> SpaceTransactionDtos { get; set; }

        public string Filter { get; set; }

        public string SpaceName { get; set; }

        public SpaceItemDto SpaceItemDto { get; set; }

        public int? ApplicantId { get; set; }

        public bool AllowNew { get; set; }
    }
}