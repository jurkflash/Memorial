using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Memorial.Core.Dtos
{
    public class PlotTransactionDto
    {
        public PlotTransactionDto()
        {

        }

        public PlotTransactionDto(int plotItemId, int plotDtoId, int applicantDtoId)
        {
            PlotItemId = plotItemId;
            PlotDtoId = plotDtoId;
            ApplicantDtoId = applicantDtoId;
        }
        public string AF { get; set; }

        public float Price { get; set; }

        public float? Maintenance { get; set; }

        public float? Wall { get; set; }

        public float? Dig { get; set; }

        public float? Brick { get; set; }

        public float Total { get; set; }

        public string Remark { get; set; }

        public int PlotItemId { get; set; }

        public PlotDto PlotDto { get; set; }

        public int PlotDtoId { get; set; }

        public int? FengShuiMasterId { get; set; }

        public ApplicantDto ApplicantDto { get; set; }

        public int ApplicantDtoId { get; set; }

        public DeceasedDto DeceasedDto1 { get; set; }

        public int? DeceasedDto1Id { get; set; }

        public DeceasedDto DeceasedDto2 { get; set; }

        public int? DeceasedDto2Id { get; set; }

        public DateTime CreateDate { get; set; }

    }
}