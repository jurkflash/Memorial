using System;
using System.Collections.Generic;
using System.Linq;
using Memorial.Core.Domain;
using Memorial.Core.Dtos;

namespace Memorial.ViewModels
{
    public class CremationOrderFormViewModel
    {
        public IEnumerable<FuneralCompanyDto> FuneralCompanyDtos { get; set; }
        public IEnumerable<DeceasedBriefDto> DeceasedBriefDtos { get; set; }
        public CremationTransactionDto CremationTransactionDto { get; set; }
    }
}