using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Memorial.Core.Domain;
using Memorial.Core.Dtos;

namespace Memorial.ViewModels
{
    public class SpaceTransactionsInfoViewModel
    {
        public SpaceTransactionDto SpaceTransactionDto { get; set; }

        public string ItemName { get; set; }

    }
}