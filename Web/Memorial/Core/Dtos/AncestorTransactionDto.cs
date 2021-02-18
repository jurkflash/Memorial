using System;
using Memorial.Core.Domain;

namespace Memorial.Core.Dtos
{
    public class AncestorTransactionDto
    {
        public string AF { get; set; }

        public AncestorItem AncestorItem { get; set; }

        public int AncestorItemId { get; set; }

        public Ancestor Ancestor { get; set; }

        public int AncestorId { get; set; }

        public string Label { get; set; }

        public string Remark { get; set; }

        public float Price { get; set; }

        public Applicant Applicant { get; set; }

        public int ApplicantId { get; set; }

        public RelationshipType RelationshipType { get; set; }

        public byte RelationshipTypeId { get; set; }

        public Deceased Deceased { get; set; }

        public int? DeceasedId { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime? ModifyDate { get; set; }

        public DateTime? DeleteDate { get; set; }
    }
}