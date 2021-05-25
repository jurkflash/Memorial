﻿using System;

namespace Memorial.Core.Dtos
{
    public class CemeteryTransactionDto
    {
        public CemeteryTransactionDto()
        {

        }

        public CemeteryTransactionDto(int cemeteryItemId, int plotDtoId, int applicantDtoId)
        {
            CemeteryItemId = cemeteryItemId;
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

        public int CemeteryItemId { get; set; }

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

        public DateTime CreateDate { get; set; }

    }
}