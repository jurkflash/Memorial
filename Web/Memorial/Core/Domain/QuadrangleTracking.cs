using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Memorial.Core.Domain
{
    public class QuadrangleTracking
    {
        public int Id { get; set; }

        public Quadrangle Quadrangle { get; set; }

        public int QuadrangleId { get; set; }

        public QuadrangleTransaction QuadrangleTransaction { get; set; }

        public string QuadrangleTransactionAF { get; set; }

        public Applicant Applicant { get; set; }

        public int? ApplicantId { get; set; }

        public Deceased Deceased1 { get; set; }

        public int? Deceased1Id { get; set; }

        public Deceased Deceased2 { get; set; }

        public int? Deceased2Id { get; set; }

        public DateTime ActionDate { get; set; }

    }
}