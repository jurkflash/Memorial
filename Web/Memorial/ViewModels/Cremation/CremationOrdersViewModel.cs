using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Memorial.Core.Domain;
using Memorial.Core.Dtos;

namespace Memorial.ViewModels
{
    public class CremationOrdersViewModel
    {
        public IEnumerable<CremationTransactionDto> CremationTransactionDtos { get; set; }
        public int CremationItemId { get; set; }
        public int ApplicantId { get; set; }
    }
}