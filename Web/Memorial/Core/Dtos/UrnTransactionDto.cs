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

        public UrnItem UrnItem { get; set; }

        public int UrnItemId { get; set; }

        public Applicant Applicant { get; set; }

        public int ApplicantId { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime? ModifyDate { get; set; }

        public DateTime? DeleteDate { get; set; }
    }
}