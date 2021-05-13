﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Memorial.Core.Domain;
using Memorial.Core.Dtos;

namespace Memorial.ViewModels
{
    public class AncestorTransactionsInfoViewModel
    {
        public AncestorTransactionDto AncestorTransactionDto { get; set; }

        public string ItemName { get; set; }

        public AncestorDto AncestorDto { get; set; }

        public int ApplicantId { get; set; }

        public int? DeceasedId { get; set; }
    }
}