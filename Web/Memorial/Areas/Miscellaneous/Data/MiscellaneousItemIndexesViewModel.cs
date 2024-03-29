﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Memorial.Core.Domain;
using Memorial.Core.Dtos;
using PagedList;

namespace Memorial.ViewModels
{
    public class MiscellaneousItemIndexesViewModel
    {
        public IPagedList<MiscellaneousTransactionDto> MiscellaneousTransactionDtos { get; set; }

        public string Filter { get; set; }

        public MiscellaneousItemDto MiscellaneousItemDto { get; set; }

        public int? ApplicantId { get; set; }

        public bool AllowNew { get; set; }

        public bool OrderFlag { get; set; }
    }
}