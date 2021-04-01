using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Memorial.Core.Domain;
using Memorial.Core.Dtos;

namespace Memorial.ViewModels
{
    public class AncestorTransactionsFormViewModel
    {
        public IEnumerable<DeceasedBriefDto> DeceasedBriefDtos { get; set; }

        public AncestorTransactionDto AncestorTransactionDto { get; set; }

        public ApplicantDto ApplicantDto { get; set; }

        public AncestorDto AncestorDto { get; set; }
    }
}