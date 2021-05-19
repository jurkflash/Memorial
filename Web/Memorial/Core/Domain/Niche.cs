using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Memorial.Core.Domain
{
    public class Niche
    {
        public Niche()
        {
            Deceaseds = new HashSet<Deceased>();

            ColumbariumTransactions1 = new HashSet<ColumbariumTransaction>();

            ColumbariumTransactions2 = new HashSet<ColumbariumTransaction>();

            ColumbariumTrackings = new HashSet<ColumbariumTracking>();
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

        public NicheType NicheType { get; set; }

        public byte NicheTypeId { get; set; }

        public ColumbariumArea ColumbariumArea { get; set; }

        public int ColumbariumAreaId { get; set; }

        public Applicant Applicant { get; set; }

        public int? ApplicantId { get; set; }

        public bool hasDeceased { get; set; }

        public bool hasFreeOrder { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime? ModifyDate { get; set; }

        public DateTime? DeleteDate { get; set; }

        public ICollection<Deceased> Deceaseds { get; set; }

        public ICollection<ColumbariumTransaction> ColumbariumTransactions1 { get; set; }

        public ICollection<ColumbariumTransaction> ColumbariumTransactions2 { get; set; }

        public ICollection<ColumbariumTracking> ColumbariumTrackings { get; set; }
    }
}