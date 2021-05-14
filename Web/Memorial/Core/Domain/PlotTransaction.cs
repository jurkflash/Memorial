﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Memorial.Core.Domain
{
    public class PlotTransaction
    {
        public PlotTransaction()
        {
            Invoices = new HashSet<Invoice>();

            Receipts = new HashSet<Receipt>();

            PlotTrackings = new HashSet<PlotTracking>();
        }

        public string AF { get; set; }

        public float Price { get; set; }

        public float? Maintenance { get; set; }

        public float? Wall { get; set; }

        public float? Dig { get; set; }

        public float? Brick { get; set; }

        public float Total { get; set; }

        public string Remark { get; set; }

        public PlotItem PlotItem { get; set; }

        public int PlotItemId { get; set; }

        public Plot Plot { get; set; }

        public int PlotId { get; set; }

        public FengShuiMaster FengShuiMaster { get; set; }

        public int? FengShuiMasterId { get; set; }

        public Applicant Applicant { get; set; }

        public int ApplicantId { get; set; }

        public Deceased Deceased1 { get; set; }

        public int? Deceased1Id { get; set; }

        public Deceased Deceased2 { get; set; }

        public int? Deceased2Id { get; set; }

        public Deceased Deceased3 { get; set; }

        public int? Deceased3Id { get; set; }

        public Applicant ClearedApplicant { get; set; }

        public int? ClearedApplicantId { get; set; }

        public Applicant TransferredApplicant { get; set; }

        public int? TransferredApplicantId { get; set; }

        public PlotTransaction TransferredPlotTransaction { get; set; }

        public string TransferredPlotTransactionAF { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime? ModifyDate { get; set; }

        public DateTime? DeleteDate { get; set; }

        public ICollection<Invoice> Invoices { get; set; }

        public ICollection<Receipt> Receipts { get; set; }

        public ICollection<PlotTracking> PlotTrackings { get; set; }
    }
}