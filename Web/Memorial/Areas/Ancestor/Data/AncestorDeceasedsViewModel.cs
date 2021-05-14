﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Memorial.Core.Domain;
using Memorial.Core.Dtos;

namespace Memorial.ViewModels
{
    public class AncestorDeceasedsViewModel
    {
        public AncestorTransactionDto AncestorTransactionDto { get; set; }

        public AncestorDto AncestorDto { get; set; }

        public ApplicantDto ApplicantDto { get; set; }

        public IEnumerable<DeceasedBriefDto> AvailableDeceaseds { get; set; }

        public int? Deceased1Id { get; set; }

        public ApplicantDeceasedFlattenDto DeceasedFlatten1Dto { get; set; }

    }
}