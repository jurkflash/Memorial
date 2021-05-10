using System;
using System.Collections.Generic;
using System.Linq;
using Memorial.Core.Domain;

namespace Memorial.Core.Dtos
{
    public class QuadrangleTransactionDto
    {
        public QuadrangleTransactionDto()
        {

        }

        public QuadrangleTransactionDto(int quadrangleItemId, int quadrangleId, int applicantId)
        {
            QuadrangleItemId = quadrangleItemId;
            QuadrangleId = quadrangleId;
            ApplicantId = applicantId;
        }

        public string AF { get; set; }

        public float Price { get; set; }

        public float? Maintenance { get; set; }

        public float? LifeTimeMaintenance { get; set; }

        public DateTime? FromDate { get; set; }

        public DateTime? ToDate { get; set; }

        public string Text1 { get; set; }

        public string Text2 { get; set; }

        public string Text3 { get; set; }

        public string Remark { get; set; }

        public QuadrangleItem QuadrangleItem { get; set; }

        public int QuadrangleItemId { get; set; }

        public Quadrangle Quadrangle { get; set; }

        public int QuadrangleId { get; set; }

        public FuneralCompany FuneralCompany { get; set; }

        public int? FuneralCompanyId { get; set; }

        public Applicant Applicant { get; set; }

        public int ApplicantId { get; set; }

        public Deceased Deceased1 { get; set; }

        public int? Deceased1Id { get; set; }

        public Deceased Deceased2 { get; set; }

        public int? Deceased2Id { get; set; }

        public Quadrangle ShiftedQuadrangle { get; set; }

        public int? ShiftedQuadrangleId { get; set; }

        public int? TransferredFromApplicantId { get; set; }

        public string ShiftedQuadrangleTransactionAF { get; set; }

        public int? TransferredApplicantId { get; set; }

        public string TransferredQuadrangleTransactionAF { get; set; }

        public DateTime CreateDate { get; set; }

    }
}