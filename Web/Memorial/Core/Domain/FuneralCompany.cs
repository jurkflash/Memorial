using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Memorial.Core.Domain
{
    public class FuneralCompany
    {
        public FuneralCompany()
        {
            SpaceTransactions = new HashSet<SpaceTransaction>();

            QuadrangleTransactions = new HashSet<QuadrangleTransaction>();

            CremationTransactions = new HashSet<CremationTransaction>();
        }

        public int Id { get; set; }

        [Required]
        [MaxLength(255)]
        public string Name { get; set; }

        [MaxLength(255)]
        public string ContactPerson { get; set; }

        [MaxLength(255)]
        public string ContactNumber { get; set; }

        [MaxLength(255)]
        public string Remark { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime? ModifyDate { get; set; }

        public DateTime? DeleteDate { get; set; }

        public ICollection<SpaceTransaction> SpaceTransactions { get; set; }

        public ICollection<QuadrangleTransaction> QuadrangleTransactions { get; set; }

        public ICollection<CremationTransaction> CremationTransactions { get; set; }
    }
}