using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Memorial.Core.Domain
{
    public class Applicant
    {
        public Applicant()
        {
            ApplicantDeceaseds = new HashSet<ApplicantDeceased>();

            AncestralTablets = new HashSet<AncestralTablet>();

            Niches = new HashSet<Niche>();

            Plots = new HashSet<Plot>();

            SpaceTransactions = new HashSet<SpaceTransaction>();

            MiscellaneousTransactions = new HashSet<MiscellaneousTransaction>();

            ColumbariumTransactions1 = new HashSet<ColumbariumTransaction>();

            ColumbariumTransactions2 = new HashSet<ColumbariumTransaction>();

            ColumbariumTransactions3 = new HashSet<ColumbariumTransaction>();

            CemeteryTransactions1 = new HashSet<CemeteryTransaction>();

            CemeteryTransactions2 = new HashSet<CemeteryTransaction>();

            CemeteryTransactions3 = new HashSet<CemeteryTransaction>();

            UrnTransactions = new HashSet<UrnTransaction>();

            CremationTransactions = new HashSet<CremationTransaction>();

            AncestralTabletTransactions = new HashSet<AncestralTabletTransaction>();

            ColumbariumTrackings = new HashSet<ColumbariumTracking>();

            AncestralTabletTrackings = new HashSet<AncestralTabletTracking>();

            PlotTrackings = new HashSet<PlotTracking>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(20)]
        public string IC { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        public string Name2 { get; set; }

        public string Address { get; set; }

        public string HousePhone { get; set; }

        public string MobileNumber { get; set; }

        public string Remark { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime? ModifyDate { get; set; }

        public DateTime? DeleteDate { get; set; }

        public ICollection<ApplicantDeceased> ApplicantDeceaseds { get; set; }

        public ICollection<AncestralTablet> AncestralTablets { get; set; }

        public ICollection<Niche> Niches { get; set; }

        public ICollection<Plot> Plots { get; set; }

        public ICollection<SpaceTransaction> SpaceTransactions { get; set; }

        public ICollection<MiscellaneousTransaction> MiscellaneousTransactions { get; set; }

        public ICollection<ColumbariumTransaction> ColumbariumTransactions1 { get; set; }

        public ICollection<ColumbariumTransaction> ColumbariumTransactions2 { get; set; }

        public ICollection<ColumbariumTransaction> ColumbariumTransactions3 { get; set; }

        public ICollection<CemeteryTransaction> CemeteryTransactions1 { get; set; }

        public ICollection<CemeteryTransaction> CemeteryTransactions2 { get; set; }

        public ICollection<CemeteryTransaction> CemeteryTransactions3 { get; set; }

        public ICollection<UrnTransaction> UrnTransactions { get; set; }

        public ICollection<CremationTransaction> CremationTransactions { get; set; }

        public ICollection<AncestralTabletTransaction> AncestralTabletTransactions { get; set; }

        public ICollection<ColumbariumTracking> ColumbariumTrackings { get; set; }

        public ICollection<AncestralTabletTracking> AncestralTabletTrackings { get; set; }

        public ICollection<PlotTracking> PlotTrackings { get; set; }
    }
}