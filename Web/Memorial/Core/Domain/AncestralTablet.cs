﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Memorial.Core.Domain
{
    public class AncestralTablet : Base
    {
        public AncestralTablet()
        {
            Deceaseds = new HashSet<Deceased>();

            AncestralTabletTransactions = new HashSet<AncestralTabletTransaction>();

            ShiftedAncestralTabletTransactions = new HashSet<AncestralTabletTransaction>();

            AncestralTabletTrackings = new HashSet<AncestralTabletTracking>();
        }

        public int Id { get; set; }

        public byte PositionX { get; set; }

        public byte PositionY { get; set; }

        public string Name { get; set; }

        public float Price { get; set; }

        public float Maintenance { get; set; }

        public string Remark { get; set; }

        public Applicant Applicant { get; set; }

        public int? ApplicantId { get; set; }

        public AncestralTabletArea AncestralTabletArea { get; set; }

        public int AncestralTabletAreaId { get; set; }

        public bool hasDeceased { get; set; }

        public bool hasFreeOrder { get; set; }

        public ICollection<Deceased> Deceaseds { get; set; }

        public ICollection<AncestralTabletTransaction> AncestralTabletTransactions { get; set; }

        public ICollection<AncestralTabletTransaction> ShiftedAncestralTabletTransactions { get; set; }

        public ICollection<AncestralTabletTracking> AncestralTabletTrackings { get; set; }
    }
}