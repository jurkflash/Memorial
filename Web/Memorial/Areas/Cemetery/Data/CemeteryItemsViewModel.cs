using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Memorial.Core.Domain;
using Memorial.Core.Dtos;

namespace Memorial.ViewModels
{
    public class CemeteryItemsViewModel
    {
        public IEnumerable<CemeteryItemDto> CemeteryItemDtos { get; set; }

        public PlotDto PlotDto { get; set; }

        public int? ApplicantId { get; set; }
        public CemeteryAreaDto CemeteryAreaDto { get; set; }

    }
}