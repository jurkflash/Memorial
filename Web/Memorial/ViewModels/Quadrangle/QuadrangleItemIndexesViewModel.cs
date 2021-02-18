using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Memorial.Core.Domain;
using Memorial.Core.Dtos;

namespace Memorial.ViewModels
{
    public class QuadrangleItemIndexesViewModel
    {
        public IEnumerable<QuadrangleTransactionDto> QuadrangleTransactionDtos { get; set; }

        public int QuadrangleItemId { get; set; }

        public int QuadrangleId { get; set; }

        public int ApplicantId { get; set; }

        public bool OrderFlag { get; set; }

        public string SystemCode { get; set; }
    }
}