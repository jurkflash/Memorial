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

            Ancestors = new HashSet<Ancestor>();

            Quadrangles = new HashSet<Quadrangle>();

            Plots = new HashSet<Plot>();

            SpaceTransactions = new HashSet<SpaceTransaction>();

            MiscellaneousTransactions = new HashSet<MiscellaneousTransaction>();

            ColumbariumTransactions1 = new HashSet<ColumbariumTransaction>();

            ColumbariumTransactions2 = new HashSet<ColumbariumTransaction>();

            ColumbariumTransactions3 = new HashSet<ColumbariumTransaction>();

            PlotTransactions1 = new HashSet<PlotTransaction>();

            PlotTransactions2 = new HashSet<PlotTransaction>();

            PlotTransactions3 = new HashSet<PlotTransaction>();

            UrnTransactions = new HashSet<UrnTransaction>();

            CremationTransactions = new HashSet<CremationTransaction>();

            AncestorTransactions = new HashSet<AncestorTransaction>();

            QuadrangleTrackings = new HashSet<QuadrangleTracking>();

            AncestorTrackings = new HashSet<AncestorTracking>();

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

        public ICollection<Ancestor> Ancestors { get; set; }

        public ICollection<Quadrangle> Quadrangles { get; set; }

        public ICollection<Plot> Plots { get; set; }

        public ICollection<SpaceTransaction> SpaceTransactions { get; set; }

        public ICollection<MiscellaneousTransaction> MiscellaneousTransactions { get; set; }

        public ICollection<ColumbariumTransaction> ColumbariumTransactions1 { get; set; }

        public ICollection<ColumbariumTransaction> ColumbariumTransactions2 { get; set; }

        public ICollection<ColumbariumTransaction> ColumbariumTransactions3 { get; set; }

        public ICollection<PlotTransaction> PlotTransactions1 { get; set; }

        public ICollection<PlotTransaction> PlotTransactions2 { get; set; }

        public ICollection<PlotTransaction> PlotTransactions3 { get; set; }

        public ICollection<UrnTransaction> UrnTransactions { get; set; }

        public ICollection<CremationTransaction> CremationTransactions { get; set; }

        public ICollection<AncestorTransaction> AncestorTransactions { get; set; }

        public ICollection<QuadrangleTracking> QuadrangleTrackings { get; set; }

        public ICollection<AncestorTracking> AncestorTrackings { get; set; }

        public ICollection<PlotTracking> PlotTrackings { get; set; }
    }
}