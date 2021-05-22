using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Memorial.Core.Domain
{
    public class AncestralTabletTracking
    {
        public int Id { get; set; }

        public Ancestor Ancestor { get; set; }

        public int AncestorId { get; set; }

        public AncestralTabletTransaction AncestralTabletTransaction { get; set; }

        public string AncestralTabletTransactionAF { get; set; }

        public Applicant Applicant { get; set; }

        public int? ApplicantId { get; set; }

        public Deceased Deceased { get; set; }

        public int? DeceasedId { get; set; }

        public DateTime ActionDate { get; set; }

    }
}