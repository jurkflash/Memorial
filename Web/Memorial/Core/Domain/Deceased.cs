using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Memorial.Core.Domain
{
    public class Deceased
    {
        public Deceased()
        {
            ApplicantDeceaseds = new HashSet<ApplicantDeceased>();

            SpaceTransactions = new HashSet<SpaceTransaction>();

            CremationTransactions = new HashSet<CremationTransaction>();

            AncestralTabletTransactions = new HashSet<AncestralTabletTransaction>();

            ColumbariumTransactions1 = new HashSet<ColumbariumTransaction>();

            ColumbariumTransactions2 = new HashSet<ColumbariumTransaction>();

            ColumbariumTrackings1 = new HashSet<ColumbariumTracking>();

            ColumbariumTrackings2 = new HashSet<ColumbariumTracking>();

            AncestralTabletTrackings = new HashSet<AncestralTabletTracking>();

            CemeteryTransactions1 = new HashSet<CemeteryTransaction>();

            CemeteryTransactions2 = new HashSet<CemeteryTransaction>();

            CemeteryTransactions3 = new HashSet<CemeteryTransaction>();

            PlotTrackings1 = new HashSet<PlotTracking>();

            PlotTrackings2 = new HashSet<PlotTracking>();

            PlotTrackings3 = new HashSet<PlotTracking>();
        }

        public int Id { get; set; }

        public string IC { get; set; }

        public string Name { get; set; }

        public string Name2 { get; set; }

        public byte Age { get; set; }

        public string Address { get; set; }

        public string Remark { get; set; }

        public GenderType GenderType { get; set; }

        public byte GenderTypeId { get; set; }

        public string Province { get; set; }

        public NationalityType NationalityType { get; set; }

        public byte NationalityTypeId { get; set; }

        public MaritalType MaritalType { get; set; }

        public byte MaritalTypeId { get; set; }

        public ReligionType ReligionType { get; set; }

        public byte ReligionTypeId { get; set; }

        public ICollection<ApplicantDeceased> ApplicantDeceaseds { get; set; }

        public string DeathPlace { get; set; }

        public DateTime DeathDate { get; set; }

        public string DeathRegistrationCentre { get; set; }

        public string DeathCertificate { get; set; }

        public string BurialCertificate { get; set; }

        public string ImportPermitNumber { get; set; }

        public Niche Niche { get; set; }

        public int? NicheId { get; set; }

        public Plot Plot { get; set; }

        public int? PlotId { get; set; }

        public Ancestor Ancestor { get; set; }

        public int? AncestorId { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime? ModifyDate { get; set; }

        public DateTime? DeleteDate { get; set; }

        public ICollection<SpaceTransaction> SpaceTransactions { get; set; }

        public ICollection<CremationTransaction> CremationTransactions { get; set; }

        public ICollection<AncestralTabletTransaction> AncestralTabletTransactions { get; set; }

        public ICollection<ColumbariumTransaction> ColumbariumTransactions1 { get; set; }

        public ICollection<ColumbariumTransaction> ColumbariumTransactions2 { get; set; }

        public ICollection<ColumbariumTracking> ColumbariumTrackings1 { get; set; }

        public ICollection<ColumbariumTracking> ColumbariumTrackings2 { get; set; }

        public ICollection<AncestralTabletTracking> AncestralTabletTrackings { get; set; }

        public ICollection<CemeteryTransaction> CemeteryTransactions1 { get; set; }

        public ICollection<CemeteryTransaction> CemeteryTransactions2 { get; set; }

        public ICollection<CemeteryTransaction> CemeteryTransactions3 { get; set; }

        public ICollection<PlotTracking> PlotTrackings1 { get; set; }

        public ICollection<PlotTracking> PlotTrackings2 { get; set; }

        public ICollection<PlotTracking> PlotTrackings3 { get; set; }

    }
}