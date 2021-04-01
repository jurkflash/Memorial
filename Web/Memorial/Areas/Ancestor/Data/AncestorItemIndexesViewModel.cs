using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Memorial.Core.Domain;
using Memorial.Core.Dtos;

namespace Memorial.ViewModels
{
    public class AncestorItemIndexesViewModel
    {
        public IEnumerable<AncestorTransactionDto> AncestorTransactionDtos { get; set; }

        public int AncestorItemId { get; set; }

        public AncestorDto AncestorDto { get; set; }

        public int AncestorId { get; set; }

        public int ApplicantId { get; set; }

        public bool OrderFlag { get; set; }

        public string SystemCode { get; set; }

        public bool AllowNew { get; set; }
    }
}