using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Memorial.Core.Domain
{
    public class FuneralCompany : Base
    {
        public FuneralCompany()
        {
            SpaceTransactions = new HashSet<SpaceTransaction>();

            ColumbariumTransactions = new HashSet<ColumbariumTransaction>();

            CremationTransactions = new HashSet<CremationTransaction>();

            CemeteryTransactions = new HashSet<CemeteryTransaction>();
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

        public ICollection<SpaceTransaction> SpaceTransactions { get; set; }

        public ICollection<ColumbariumTransaction> ColumbariumTransactions { get; set; }

        public ICollection<CremationTransaction> CremationTransactions { get; set; }

        public ICollection<CemeteryTransaction> CemeteryTransactions { get; set; }
    }
}