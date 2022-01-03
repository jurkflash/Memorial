using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Memorial.Core.Domain;
using Memorial.Core.Dtos;

namespace Memorial.ViewModels
{
    public class ColumbariumItemsViewModel
    {
        public IEnumerable<ColumbariumItemDto> ColumbariumItemDtos { get; set; }

        public NicheDto NicheDto { get; set; }

        public int? ApplicantId { get; set; }

    }
}