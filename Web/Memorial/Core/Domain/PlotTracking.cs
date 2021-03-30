using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Memorial.Core.Domain
{
    public class PlotTracking
    {
        public int Id { get; set; }

        public Plot Plot { get; set; }

        public int PlotId { get; set; }

        public PlotTransaction PlotTransaction { get; set; }

        public string PlotTransactionAF { get; set; }

        public Applicant Applicant { get; set; }

        public int? ApplicantId { get; set; }

        public Deceased Deceased1 { get; set; }

        public int? Deceased1Id { get; set; }

        public Deceased Deceased2 { get; set; }

        public int? Deceased2Id { get; set; }

        public DateTime ActionDate { get; set; }

    }
}