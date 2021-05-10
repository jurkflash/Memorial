using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Memorial.Core.Domain
{
    public class Quadrangle
    {
        public Quadrangle()
        {
            Deceaseds = new HashSet<Deceased>();

            QuadrangleTransactions1 = new HashSet<QuadrangleTransaction>();

            QuadrangleTransactions2 = new HashSet<QuadrangleTransaction>();

            QuadrangleTrackings1 = new HashSet<QuadrangleTracking>();

            QuadrangleTrackings2 = new HashSet<QuadrangleTracking>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public byte PositionX { get; set; }

        public byte PositionY { get; set; }

        public float Price { get; set; }

        public float Maintenance { get; set; }

        public float LifeTimeMaintenance { get; set; }

        public string Remark { get; set; }

        public QuadrangleType QuadrangleType { get; set; }

        public byte QuadrangleTypeId { get; set; }

        public QuadrangleArea QuadrangleArea { get; set; }

        public int QuadrangleAreaId { get; set; }

        public Applicant Applicant { get; set; }

        public int? ApplicantId { get; set; }

        public bool hasDeceased { get; set; }

        public bool hasFreeOrder { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime? ModifyDate { get; set; }

        public DateTime? DeleteDate { get; set; }

        public ICollection<Deceased> Deceaseds { get; set; }

        public ICollection<QuadrangleTransaction> QuadrangleTransactions1 { get; set; }

        public ICollection<QuadrangleTransaction> QuadrangleTransactions2 { get; set; }

        public ICollection<QuadrangleTracking> QuadrangleTrackings1 { get; set; }

        public ICollection<QuadrangleTracking> QuadrangleTrackings2 { get; set; }
    }
}