﻿using System;
using System.ComponentModel.DataAnnotations;

namespace Memorial.Core.Dtos
{
    public class CemeteryTransactionDto
    {
        public CemeteryTransactionDto()
        {

        }

        public CemeteryTransactionDto(int cemeteryItemId, int plotDtoId, int applicantDtoId)
        {
            CemeteryItemDtoId = cemeteryItemId;
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

        [StringLength(255)]
        public string Remark { get; set; }

        public CemeteryItemDto CemeteryItemDto { get; set; }

        public int CemeteryItemDtoId { get; set; }

        public PlotDto PlotDto { get; set; }

        public int PlotDtoId { get; set; }

        public FengShuiMasterDto FengShuiMasterDto { get; set; }

        public int? FengShuiMasterDtoId { get; set; }

        public FuneralCompanyDto FuneralCompanyDto { get; set; }

        public int? FuneralCompanyDtoId { get; set; }

        public ApplicantDto ApplicantDto { get; set; }

        public int ApplicantDtoId { get; set; }

        public DeceasedDto DeceasedDto1 { get; set; }

        public int? DeceasedDto1Id { get; set; }

        public DeceasedDto DeceasedDto2 { get; set; }

        public int? DeceasedDto2Id { get; set; }

        public DeceasedDto DeceasedDto3 { get; set; }

        public int? DeceasedDto3Id { get; set; }

        public int? ClearedApplicantId { get; set; }

        public int? TransferredApplicantId { get; set; }

        public string TransferredCemeteryTransactionAF { get; set; }

        public string SummaryItem { get; set; }

        public DateTime? ClearanceDate { get; set; }

        public DateTime? ClearanceGroundDate { get; set; }

        public DateTime CreatedUtcTime { get; set; }

    }
}