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

            AncestorTransactions = new HashSet<AncestorTransaction>();

            ColumbariumTransactions1 = new HashSet<ColumbariumTransaction>();

            ColumbariumTransactions2 = new HashSet<ColumbariumTransaction>();

            QuadrangleTrackings1 = new HashSet<QuadrangleTracking>();

            QuadrangleTrackings2 = new HashSet<QuadrangleTracking>();

            AncestorTrackings = new HashSet<AncestorTracking>();

            PlotTransactions1 = new HashSet<PlotTransaction>();

            PlotTransactions2 = new HashSet<PlotTransaction>();

            PlotTransactions3 = new HashSet<PlotTransaction>();

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

        public Quadrangle Quadrangle { get; set; }

        public int? QuadrangleId { get; set; }

        public Plot Plot { get; set; }

        public int? PlotId { get; set; }

        public Ancestor Ancestor { get; set; }

        public int? AncestorId { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime? ModifyDate { get; set; }

        public DateTime? DeleteDate { get; set; }

        public ICollection<SpaceTransaction> SpaceTransactions { get; set; }

        public ICollection<CremationTransaction> CremationTransactions { get; set; }

        public ICollection<AncestorTransaction> AncestorTransactions { get; set; }

        public ICollection<ColumbariumTransaction> ColumbariumTransactions1 { get; set; }

        public ICollection<ColumbariumTransaction> ColumbariumTransactions2 { get; set; }

        public ICollection<QuadrangleTracking> QuadrangleTrackings1 { get; set; }

        public ICollection<QuadrangleTracking> QuadrangleTrackings2 { get; set; }

        public ICollection<AncestorTracking> AncestorTrackings { get; set; }

        public ICollection<PlotTransaction> PlotTransactions1 { get; set; }

        public ICollection<PlotTransaction> PlotTransactions2 { get; set; }

        public ICollection<PlotTransaction> PlotTransactions3 { get; set; }

        public ICollection<PlotTracking> PlotTrackings1 { get; set; }

        public ICollection<PlotTracking> PlotTrackings2 { get; set; }

        public ICollection<PlotTracking> PlotTrackings3 { get; set; }

    }
}