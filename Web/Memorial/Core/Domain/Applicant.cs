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
            Deceaseds = new HashSet<Deceased>();

            Quadrangles = new HashSet<Quadrangle>();

            Plots = new HashSet<Plot>();

            SpaceTransactions = new HashSet<SpaceTransaction>();

            MiscellaneousTransactions = new HashSet<MiscellaneousTransaction>();

            QuadrangleTransactions = new HashSet<QuadrangleTransaction>();

            PlotTransactions = new HashSet<PlotTransaction>();

            UrnTransactions = new HashSet<UrnTransaction>();

            CremationTransactions = new HashSet<CremationTransaction>();

            AncestorTransactions = new HashSet<AncestorTransaction>();
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

        public ICollection<Deceased> Deceaseds { get; set; }

        public ICollection<Quadrangle> Quadrangles { get; set; }

        public ICollection<Plot> Plots { get; set; }

        public ICollection<SpaceTransaction> SpaceTransactions { get; set; }

        public ICollection<MiscellaneousTransaction> MiscellaneousTransactions { get; set; }

        public ICollection<QuadrangleTransaction> QuadrangleTransactions { get; set; }

        public ICollection<PlotTransaction> PlotTransactions { get; set; }

        public ICollection<UrnTransaction> UrnTransactions { get; set; }

        public ICollection<CremationTransaction> CremationTransactions { get; set; }

        public ICollection<AncestorTransaction> AncestorTransactions { get; set; }
    }
}