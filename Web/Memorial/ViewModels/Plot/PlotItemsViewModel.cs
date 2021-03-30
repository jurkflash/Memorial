using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Memorial.Core.Domain;
using Memorial.Core.Dtos;

namespace Memorial.ViewModels
{
    public class PlotItemsViewModel
    {
        public IEnumerable<PlotItemDto> PlotItemDtos { get; set; }

        public PlotDto PlotDto { get; set; }

        public ApplicantDto ApplicantDto { get; set; }

    }
}