﻿using Memorial.Core.Dtos;
using PagedList;

namespace Memorial.ViewModels
{
    public class PlotItemIndexesViewModel
    {
        public IPagedList<CemeteryTransactionDto> CemeteryTransactionDtos { get; set; }

        public int PlotItemId { get; set; }

        public PlotDto PlotDto { get; set; }

        public int PlotId { get; set; }

        public int ApplicantId { get; set; }

        public bool OrderFlag { get; set; }

        public string SystemCode { get; set; }

        public bool AllowNew { get; set; }
    }
}