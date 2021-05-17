using System.Collections.Generic;
using Memorial.Core.Dtos;
using PagedList;

namespace Memorial.ViewModels
{
    public class QuadrangleItemIndexesViewModel
    {
        public IPagedList<QuadrangleTransactionDto> QuadrangleTransactionDtos { get; set; }

        public int QuadrangleItemId { get; set; }

        public QuadrangleDto QuadrangleDto { get; set; }

        public int QuadrangleId { get; set; }

        public int ApplicantId { get; set; }

        public bool OrderFlag { get; set; }

        public string SystemCode { get; set; }

        public bool AllowNew { get; set; }
    }
}