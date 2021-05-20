﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Memorial.Core.Domain
{
    public class Plot
    {
        public Plot()
        {
            Deceaseds = new HashSet<Deceased>();

            CemeteryTransactions = new HashSet<CemeteryTransaction>();

            PlotTrackings = new HashSet<PlotTracking>();

            PlotItems = new HashSet<PlotItem>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Size { get; set; }

        public float Price { get; set; }

        public float Maintenance { get; set; }

        public float Wall { get; set; }

        public float Dig { get; set; }

        public float Brick { get; set; }

        public string Remark { get; set; }

        public PlotType PlotType { get; set; }

        public byte PlotTypeId { get; set; }

        public PlotArea PlotArea { get; set; }

        public int PlotAreaId { get; set; }

        public Applicant Applicant { get; set; }

        public int? ApplicantId { get; set; }

        public bool hasDeceased { get; set; }

        public bool hasCleared { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime? ModifyDate { get; set; }

        public DateTime? DeleteDate { get; set; }

        public ICollection<Deceased> Deceaseds { get; set; }

        public ICollection<CemeteryTransaction> CemeteryTransactions { get; set; }

        public ICollection<PlotTracking> PlotTrackings { get; set; }

        public ICollection<PlotItem> PlotItems { get; set; }
    }
}