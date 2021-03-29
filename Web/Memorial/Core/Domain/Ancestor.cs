using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Memorial.Core.Domain
{
    public class Ancestor
    {
        public Ancestor()
        {
            Deceaseds = new HashSet<Deceased>();

            AncestorTransactions = new HashSet<AncestorTransaction>();

            ShiftedAncestorTransactions = new HashSet<AncestorTransaction>();

            AncestorTrackings = new HashSet<AncestorTracking>();
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

        public AncestorArea AncestorArea { get; set; }

        public int AncestorAreaId { get; set; }

        public bool hasDeceased { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime? ModifyDate { get; set; }

        public DateTime? DeleteDate { get; set; }

        public ICollection<Deceased> Deceaseds { get; set; }

        public ICollection<AncestorTransaction> AncestorTransactions { get; set; }

        public ICollection<AncestorTransaction> ShiftedAncestorTransactions { get; set; }

        public ICollection<AncestorTracking> AncestorTrackings { get; set; }
    }
}