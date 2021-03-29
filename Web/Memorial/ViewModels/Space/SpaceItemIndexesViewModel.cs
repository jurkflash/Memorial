using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Memorial.Core.Domain;
using Memorial.Core.Dtos;

namespace Memorial.ViewModels
{
    public class SpaceItemIndexesViewModel
    {
        public IEnumerable<SpaceTransactionDto> SpaceTransactionDtos { get; set; }

        public int SpaceItemId { get; set; }

        public int ApplicantId { get; set; }

        public bool AllowNew { get; set; }
    }
}