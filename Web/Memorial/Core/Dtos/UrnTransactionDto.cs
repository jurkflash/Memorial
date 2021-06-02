using System;
using System.Collections.Generic;
using System.Linq;
using Memorial.Core.Domain;

namespace Memorial.Core.Dtos
{
    public class UrnTransactionDto
    {
        public string AF { get; set; }

        public float Price { get; set; }

        public string Remark { get; set; }

        public UrnItemDto UrnItemDto { get; set; }

        public int UrnItemDtoId { get; set; }

        public ApplicantDto ApplicantDto { get; set; }

        public int ApplicantDtoId { get; set; }

        public string SummaryItem { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime? ModifyDate { get; set; }
    }
}