﻿namespace Memorial.Core.Dtos
{
    public class ApplicantDeceasedFlattenDto
    {
        public int Id { get; set; }

        public int ApplicantId { get; set; }

        public string ApplicantName { get; set; }

        public string ApplicantName2 { get; set; }

        public int DeceasedId { get; set; }

        public string DeceasedName { get; set; }

        public string DeceasedName2 { get; set; }

        public byte RelationshipTypeId { get; set; }

        public string RelationshipTypeName { get; set; }

    }
}