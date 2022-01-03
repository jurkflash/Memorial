using Memorial.Core.Dtos;
using PagedList;

namespace Memorial.ViewModels
{
    public class CemeteryItemIndexesViewModel
    {
        public IPagedList<CemeteryTransactionDto> CemeteryTransactionDtos { get; set; }

        public string Filter { get; set; }

        public CemeteryItemDto CemeteryItemDto { get; set; }
        public int CemeteryItemDtoId { get; set; }

        public PlotDto PlotDto { get; set; }

        public int PlotId { get; set; }

        public int? ApplicantId { get; set; }

        public bool OrderFlag { get; set; }

        public string SystemCode { get; set; }

        public bool AllowNew { get; set; }
    }
}