using System;
using System.ComponentModel.DataAnnotations;
using Memorial.Core.Domain;

namespace Memorial.Core.Dtos
{
    public class DeceasedBriefDto
    {
        public int Id { get; set; }

        public string IC { get; set; }

        public string Name { get; set; }

        public string Name2 { get; set; }

        public RelationshipType RelationshipType { get; set; }

        public byte RelationshipTypeId { get; set; }
    }
}