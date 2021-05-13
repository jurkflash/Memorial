﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Memorial.Core.Domain;
using Memorial.Core.Dtos;

namespace Memorial.ViewModels
{
    public class CremationItemIndexesViewModel
    {
        public IEnumerable<CremationTransactionDto> CremationTransactionDtos { get; set; }

        public int CremationItemId { get; set; }

        public int ApplicantId { get; set; }

        public bool AllowNew { get; set; }
    }
}